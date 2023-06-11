namespace BLL.Person_entities.UserFolder
{
    public enum FileType
    {
        png, jpg, bmp
    }
    public class UserPhoto
    {
        public int Id { get; set; }
        public byte[] PhotoByteCode { get; set; }
        public FileType FileType { get; set; }
        public User User { get; set; }
    }
}
