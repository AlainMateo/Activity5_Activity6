using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Activity5_Activity6.Models;
namespace Activity5_Activity6.Controllers
{
    [Authorize]
public class EmployeesController : ApiController
{
    private IPTEntities db = new IPTEntities();

    public IQueryable<EmployeeTable> GetEmployee()
    {
        return db.EmployeeTables;
    }

    [ResponseType(typeof(EmployeeTable))]
    public IHttpActionResult GetEmployee(int id)
    {
            EmployeeTable employee = db.EmployeeTables.Find(id);
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [ResponseType(typeof(void))]
    public IHttpActionResult PutEmployee(int id, EmployeeTable employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != employee.EmployeeID)
        {
            return BadRequest();
        }

        db.Entry(employee).State = EntityState.Modified;

        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return StatusCode(HttpStatusCode.NoContent);
    }

    [ResponseType(typeof(EmployeeTable))]
    public IHttpActionResult PostEmployee(EmployeeTable employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.EmployeeTables.Add(employee);
        db.SaveChanges();

        return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
    }

    [ResponseType(typeof(EmployeeTable))]
    public IHttpActionResult DeleteEmployee(int id)
    {
        EmployeeTable employee = db.EmployeeTables.Find(id);
        if (employee == null)
        {
            return NotFound();
        }

        db.EmployeeTables.Remove(employee);
        db.SaveChanges();

        return Ok(employee);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }

    private bool EmployeeExists(int id)
    {
        return db.EmployeeTables.Count(e => e.EmployeeID == id) > 0;
    }
}
}
