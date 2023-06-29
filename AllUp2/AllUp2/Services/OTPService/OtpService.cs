namespace AllUp2.Services.OTPService
{
    public  class OtpService
    {
        public static string GenerateOTP()
        {
            Random random = new Random();
            int otpnumber = random.Next(100000, 999999); // we will give squence and it will provide random number -- length will be 6
            return otpnumber.ToString();
        }
    }
}
