using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.DAL;
using System.Data;
using System.Net;
using System.Web.SessionState;

namespace MVCDemo.Controllers
{
    public class HomeController : Controller
    {
        private PostContext db = new PostContext();


        public ActionResult Home()
        {
            IEnumerable<Post> posts = DataAccess.GetAllPosts();
            ViewBag.Title = "twitter";
            if (Session["UserName"] == null)
            {
                ViewBag.Welcome = "Welcome Guest!";
            }
            else
            {
                ViewBag.Welcome = "Welcome " + Session["UserName"] + "!";
            }

            Session["ProfilePage"] = null;
            Session["ProfileID"] = 0;
            return View(posts);

            /*List<Post> posts = Repository.GetAllPosts();
            return View(posts);*/
        }

        [HttpGet]
        public ActionResult Detail(string name)
        {
            if (name == null)
            {
                return View("Error");
            }
            //Post userPost = null;
            // userPost = db.Posts.FirstOrDefault(x => x.posterName == name);
            Session["ProfilePage"] = name;
            Session["ProfileID"] = DataAccess.ReturnUserID(name);
            List<Post> allUserPosts = DataAccess.allUserPosts(name, Convert.ToInt32(Session["ProfileID"]));
            if (allUserPosts != null)
            {

                //db.Posts.Where(x => x.posterName == name).OrderByDescending(y => y.Timestamp).ToList();
                return View(allUserPosts);
            }
            else
            {
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult _Detail([Bind(Include = "postContent")]Post newPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    newPost.Timestamp = DateTime.Now;
                    newPost.posterName = Session["UserName"].ToString();
                    newPost.postedTo = Convert.ToInt32(Session["ProfileID"]);
                    newPost.posterEmail = Session["UserEmail"].ToString();
                    //DataAccess.CreatePost();
                    // db.Posts.Add(newPost);
                    // db.SaveChanges();
                    // Repository.AddToList(newPost);
                    if (DataAccess.CreatePost(newPost) == 1)
                    {
                        //List<Post> allUserPosts = DataAccess.allUserPosts(newPost.posterName, Convert.ToInt32(Session["ProfileID"]));
                        return RedirectToAction("Detail", new { name = Session["ProfilePage"].ToString() });
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }
            //return View(newPost);
            return RedirectToAction("Detail", new { name = Session["ProfilePage"].ToString() });

        }

        /*@Html.EditorFor(model => model.postContent, new { htmlAttributes = new { @placeholder = "Leave a message..." } })
            @Html.ValidationMessageFor(model => model.postContent)*/

        /*  [HttpPost]
          public ActionResult Detail([Bind(Include = "postContent")]Post WallPost)
          {
              //string postContent = WallPost.postContent;

              // Insert to DB
              //try
              // {
              // if (ModelState.IsValid)
              // {
              WallPost.Timestamp = DateTime.Now;
              WallPost.posterName = Session["UserName"].ToString();

              if (DataAccess.CreatePost(WallPost) == 1)
              {
                  return RedirectToAction("Home");
              }
              else
              {
                  return View("Error");
              }
              //}
              //else
              //    return View("Error");
              //}
              //catch (DataException)
              //{

              //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
              //    return View("Error");

              //}
              ////return View(WallPost);
              ////return View();
          }*/

        [HttpGet]
        public ActionResult Create()
        {
            Post newPost = new Post();
            ViewBag.UserName = Session["UserName"];
            return View(newPost);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "postContent")]Post newPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    newPost.Timestamp = DateTime.Now;
                    newPost.posterName = Session["UserName"].ToString();
                    newPost.posterEmail = Session["UserEmail"].ToString();
                    newPost.postedTo = Convert.ToInt32(Session["ProfileID"]);
                    //DataAccess.CreatePost();
                    // db.Posts.Add(newPost);
                    // db.SaveChanges();
                    // Repository.AddToList(newPost);
                    if (DataAccess.CreatePost(newPost) == 1)
                    {
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }
            return View(newPost);

        }


    }
}
