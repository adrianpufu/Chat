using Chat.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chat.Models;
using System.Net;

namespace Chat.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        RoomRepository _roomRepository = null;

        UserRepository _userRepository = null;

        public HomeController()
        {
            RoomRepository _roomRepository = new RoomRepository(unitOfWork.RoomRepository.context);

            UserRepository _userRepository = new UserRepository(unitOfWork.UserRepository.context);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Rooms()
        {
            var viewModel = new ConversationRoomViewModel();

            if (unitOfWork.RoomRepository.Get().Any() == false)
            {
                ConversationRoom room = new ConversationRoom()
                {
                    RoomName = "TheFirstRoom",
                    RoomType = ChatRoomTypeEnum.UserToRoom
                };
                unitOfWork.RoomRepository.Insert(room);

            }

            viewModel.ConversationRooms = unitOfWork.RoomRepository.Get();

            return View("Rooms", viewModel);
        }

        public ActionResult SingleRoom(string roomName)
        {
            if (roomName.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new SingleRoomViewModel();

            viewModel.Room = _roomRepository.GetRoomByName(roomName);

            if (viewModel.Room == null)
            {
                return HttpNotFound();
            }
            return View("SingleRoom", viewModel);
        }

        public ActionResult SingleUser(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new SingleUserViewModel();

            viewModel.User = _userRepository.GetUserByName(userName);

            viewModel.Room = _roomRepository.GetRoomByName(userName);

            if ((viewModel.User == null) || (viewModel.Room == null))
            {
                return HttpNotFound();
            }

            return View("SingleUser", viewModel);
        }
    }
}