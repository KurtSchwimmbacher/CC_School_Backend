using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Models;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Data;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDBContext _context;

        private readonly IUpdateAdminPassword _passwordService;

        private readonly IAdminAuth _adminAuth;

        public AdminController(AppDBContext context, IAdminAuth adminAuth, IUpdateAdminPassword passwordService)
        {
            _context = context;
            _adminAuth = adminAuth;
            _passwordService = passwordService;
        }


        // register admin
        [HttpPost("register")]
        public async Task<ActionResult<bool>> RegisterAdmin(AdminRegisterDTO adminRegisterDTO)
        {
            //map AdminRegisterDTO to Admin model
            Admin admin = new Admin
            {
                Name = adminRegisterDTO.FName,
                LastName = adminRegisterDTO.LName,
                Password = adminRegisterDTO.Password,
                PhoneNumber = adminRegisterDTO.phoneNumber,
                AdminRole = adminRegisterDTO.AdminRole,
                AssignedDepartments = adminRegisterDTO.Department
            };

            bool isRegistered = await _adminAuth.RegisterAdmin(admin);

            if (!isRegistered)
            {
                return BadRequest("Admin already exists");
            }

            return Ok("Admin Registered Successfully");
        }

        // login admin
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAdmin(LoginDTO admin)
        {
            string msg = await _adminAuth.LoginAdmin(admin.Password, admin.Email);
            if (msg == "Login Successful")
            {
                return Ok(msg);
            }
            else
            {
                return BadRequest(msg);
            }
        }


        [HttpPut("{studentNumber}/updatePassword")]
        public async Task<IActionResult> UpdateAdminPassword(string AdminEmail, StudentPasswordDTO PasswordDTO)
        {
            bool updated = await _passwordService.UpdateAdminPassword(AdminEmail, PasswordDTO);

            if (!updated)
            {
                return BadRequest("Password update failed. Incorrect old password or admin not found.");
            }

            return Ok("Password updated successfully.");
        }



        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }



        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
