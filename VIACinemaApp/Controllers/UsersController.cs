using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VIACinemaApp.Models.Movies
{
    public class UsersController : Controller
    {
        private readonly VIACinemaAppContext _context;

        public UsersController(VIACinemaAppContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Username,Password,PhoneNumber,Name")] User user)
        {
            if (EmailExists(user.Email))
                ModelState.AddModelError("Email", "Email already in use");

            if (UsernameExists(user.Username))
                ModelState.AddModelError("Username", "Username already in use");
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Username,Password,PhoneNumber,Name")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        /// <summary>
        /// Helper method - Email is a unique key so this method avoids throwing an exception when trying to add a duplicate value
        /// </summary>
        /// <param name="email">Email read from the input form.</param>
        /// <returns>True if the email exists in current context. False if it doesn't.</returns>
        private bool EmailExists(string email)
        {
            return _context.User.Any(e => e.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool UsernameExists(string username)
        {
            return _context.User.Any(e => e.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}