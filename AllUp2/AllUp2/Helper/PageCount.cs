namespace AllUp2.Helper
{
    public class PageCount
    {
        public static int PageCountt(int count, int take)
        {
            return (int)Math.Ceiling((decimal)count / take);
        }

        //public static void DeleteFile(string path)
        //{
        //    if (System.IO.File.Exists(path))
        //    {
        //        System.IO.File.Delete(path);
        //    }
        //}
    }
}

