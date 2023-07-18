using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.ViewModels.Home;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllUp2.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            BlogVM blogVM = RetrieveData();
            return View(blogVM);
        }
        public IActionResult Search(string search)
        {
            var blogs = _appDbContext.Blogs
                .Where(b => b.Title.ToLower().Contains(search.ToLower()))
                .Take(4)
                .ToList();

            var blogSearchResults = new BlogSearchResultVM
            {
                Blogs = blogs
            };

            return PartialView("_BlogSearchPartial", blogSearchResults);
        }
        public IActionResult Detail(int id)
        {
            Blog blog = RetrieveData().Blogs.FirstOrDefault(b => b.Id == id);
            BlogVM blogVM = RetrieveData();
            blogVM.Blog = blog;
            return View(blogVM);
        }

        private BlogVM RetrieveData()
        {
            BlogVM blogVM = new();
            blogVM.Tags = _appDbContext.Tags.AsNoTracking().ToList();
            blogVM.Blogs = _appDbContext.Blogs.Include(b => b.BlogDetail).Include(b => b.Comments).AsNoTracking().ToList();
            blogVM.Categories = _appDbContext.Categories.Include(c => c.Blogs).ToList();
            return blogVM;
        }


        [HttpPost]
        public IActionResult SubmitCommentForm(int id, Comment model)
        {
            if (model.ParentCommentId.HasValue)
            {
                // Create a new comment for the reply
                var reply = new Comment
                {
                    BlogId = id,
                    Author = model.Author,
                    Email = model.Email,
                    CommentContent = model.CommentContent,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "comment-author.jpg",
                    IsReply = true
                };

                // Retrieve the parent comment and add the new comment as a reply
                var parentComment = _appDbContext.Comments.Find(model.ParentCommentId);
                if (parentComment != null)
                {
                    parentComment.Replies ??= new List<Comment>();
                    parentComment.Replies.Add(reply);
                }
            }
            else
            {
                // Create a new comment for the main form submission
                var newComment = new Comment
                {
                    BlogId = id,
                    Author = model.Author,
                    Email = model.Email,
                    CommentContent = model.CommentContent,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "comment-author.jpg",
                    IsReply = false
                };

                _appDbContext.Comments.Add(newComment);
            }

            _appDbContext.SaveChanges();

            return RedirectToAction("Detail", new { id });
        }
    }
}
