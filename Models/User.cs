namespace WebApi.Models;
public class User
{
    //Properties เป็นการกำหนดข้อมูลที่ต้องการเก็บไว้ใน Class นี้
    public int Id { get; set; }
    public required string Username { get; set; } //required เป็นการกำหนดให้ Properties นี้เป็น Not Null
    public required string Email { get; set; }
    public required string FullName { get; set; }
}