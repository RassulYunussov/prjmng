using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjmng.Models;
using prjmng.ViewModels;

namespace prjmng.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController: ControllerBase
    {
        ApplicationDbContext _ctx;
        public ProjectController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get(int managerId)
        {
            var projects = await _ctx.Projects.Where(p=>p.ProjectManager.Id==managerId)
                                                .Select(p=>new {p.Id, p.Name})
                                                .ToListAsync();
            return projects;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<object>> Details(int id)
        {
            var project = await _ctx.Projects.FindAsync(id);
                                    
            return new {name = project.Name};
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectViewModel project)
        {
            if(ModelState.IsValid)
            {
              Project p = new Project(){Name = project.Name};
              p.ProjectManager = await _ctx.Managers.FindAsync(project.ManagerId);
              if(p.ProjectManager!=null)
              {
                   _ctx.Projects.Add(p);
                   await _ctx.SaveChangesAsync();
                   return Ok( new {id = p.Id,name =p.Name} );
              }
              else
              {
                  return BadRequest(new {message = "Unknown Manager Id"});
              }
             
            }
            else 
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProjectViewModel projectViewModel)
        {
            if(ModelState.IsValid)
            {
                Project p = new Project(){Id = id,Name = projectViewModel.Name};
                p.ProjectManager = await _ctx.Managers.FindAsync(projectViewModel.ManagerId);
                _ctx.Projects.Update(p);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else 
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Project p = await _ctx.Projects.FindAsync(id);
            if(p!=null)
            {
                _ctx.Remove(p);
                _ctx.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}