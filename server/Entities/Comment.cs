namespace Entities;

public class Comment
{
    public int ID { get; set; }
    public String Body { get; set; }
    public int PostID { get; set; }
    public int UserID { get; set; }
}