namespace AllUp2.Helper
{
    public class PageCount
    {
        public static int PageCountt(int count, int take)
        {
            return (int)Math.Ceiling((decimal)count / take);
        }
    }
}

