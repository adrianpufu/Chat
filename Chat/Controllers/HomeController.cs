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
        private UserContext db = null;

        public HomeController()
        {
            db = new UserContext();
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

            if (db.Rooms == null)
            {
                var room = db.Rooms.Create();
                room.RoomName = "TheFirstRoom";
                room.RoomType = ChatRoomTypeEnum.UserToRoom;

                db.Rooms.Add(room);
                db.SaveChanges();
            }
            viewModel.ConversationRooms = db.Rooms.ToList();

            return View("Rooms", viewModel);
        }

        public ActionResult SingleRoom(string roomName)
        {
            if (roomName.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new SingleRoomViewModel();

            viewModel.Room = db.Rooms.Find(roomName);

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

            viewModel.User = db.Users.Find(userName);

            viewModel.Room = db.Rooms.Find(userName);

            if ((viewModel.User == null) || (viewModel.Room == null))
            {
                return HttpNotFound();
            }

            return View("SingleUser", viewModel);
        }
    }
}