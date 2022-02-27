using Twilio;
using Twilio.Rest.Api.V2010.Account;


class Program
{
    private static void SendSms(string to)
    {
        try
        {
            var message = MessageResource.Create(
                body: "Ищи своих знакомых здесь: 200rf.com \nВыходи на митинги против войны, останови путина!",
                from: new Twilio.Types.PhoneNumber("+19108129574"),
                to: new Twilio.Types.PhoneNumber(to)
            );
            Console.WriteLine(message.Sid);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    static void Main()
    {
        string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

        TwilioClient.Init(accountSid, authToken);
        var numbers = File.ReadAllLines("./phone-numbers.txt");
        numbers = numbers.Where(n => n.Length == 12 && n.StartsWith("+7")).Distinct().ToArray();
        foreach (var to in numbers)
            SendSms(to);
    }
}
