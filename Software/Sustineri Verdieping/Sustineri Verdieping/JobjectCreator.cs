namespace Sustineri_Verdieping
{
    /// <summary>
    /// Used to create json objects.
    /// </summary>
    class JobjectCreator
    {
        // Sensor
        public string Sensor_ID { get; set; }
        public string Value { get; set; }
        public string DateWeek { get; set; }
        public string DateMonth { get; set; }
        public string DateYear { get; set; }
        public string SetLimit { get; set; }

        // User
        public string User_ID { get; set; }
        public string Firstname { get; set; }
        public string Insertion { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string New_Email { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string New_Password { get; set; }
        public string New_Password2 { get; set; }

        // Token
        public string Device_Pass { get; set; }
        public string JWT_Token { get; set; }

        // Delete
        public string Delete_Confirmation { get; set; }
    }
}
