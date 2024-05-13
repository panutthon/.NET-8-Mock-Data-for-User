using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]                 //เป็นการกำหนดว่า Class นี้เป็น Controller ในการรับ Request และ Response ทำ API
[Route("api/[controller]")]      //เป็นการกำหนด URL ของ Controller นี้ api/User
public class UserController : Controller
{
    //Mock Data for User สร้างข้อมูล Mock ขึ้นมาเพื่อทดสอบการส่งค่ากลับไปหา Client
    private static readonly List<User> _users = new List<User>
    {
        new User {
            Id = 1,
            Username = "admin",
            Email = "admin@localhost",
            FullName = "Administrator"
        },
        new User {
            Id = 2,
            Username = "user",
            Email = "user@localhost",
            FullName = "User"
        }
    };



    //GET api/User
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUser()  //ActionResult คือการส่งค่ากลับไปหา Client โมเดลที่ส่งกลับไปคือ List<User>
    //IEnumerable เป็น Interface ที่ใช้ในการวนลูปข้อมูลที่เก็บไว้ใน List หรือ Array  ใช้แทน collection ของข้อมูล 
    {
        return Ok(_users);  //ส่งค่ากลับไปหา Client โดยใช้ Ok และส่งค่ากลับไปเป็น List<User>
    }

    // get user by id
    // get api/User/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)  //ActionResult คือการส่งค่ากลับไปหา Client โมเดลที่ส่งกลับไปคือ User
    {
        // LINQ คืออะไร
        // คือการใช้คำสั่งในการค้นหาข้อมูลใน List หรือ Array โดยไม่ต้องใช้ For Loop
        // FirstOrDefault คือการค้นหาข้อมูลใน List หรือ Array โดยใช้เงื่อนไขที่กำหนด และส่งค่ากลับไปเป็น Object หรือ Null ถ้าไม่พบข้อมูล

        var user = _users.Find(u => u.Id == id);  //ค้นหาข้อมูล User จาก List<User> โดยใช้ Id ที่ส่งมา
        if (user == null)  //ถ้าไม่พบข้อมูล User จะส่งค่ากลับไปเป็น NotFound
        {
            return NotFound();
        }
        return Ok(user);  //ส่งค่ากลับไปหา Client โดยใช้ Ok และส่งค่ากลับไปเป็น User
    }

    // create new user
    // POST api/User

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    //Update User by Id
    //put api/User/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        //Validate Request
        if (id != user.Id)
        {
            return BadRequest();
        }

        //Find existing user
        var existingUser = _users.Find(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        //Update User
        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.FullName = user.FullName;

        // return updated user
        return Ok(existingUser);
    }

    //Delete User by Id
    //Delete api/User/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        //Find existing user
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        _users.Remove(user);
        return NoContent();
    }
}

