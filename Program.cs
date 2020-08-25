using System;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace email_send_example
{
  class Program
  {
    static void Main(string[] args)
    {
      var auth = ConfidentialClientApplicationBuilder
          .Create("1d102953-2f1d-47a6-82b9-459ef2bb61aa")
          .WithClientSecret("~kJdJ57_20rqw.vgvwzf0dP00T5t30_lqR")
          .WithTenantId("fa816c4a-0330-403f-b134-6c4cf830ec1f")
          .Build();


      var provider = new TokenProvider(auth);
      var client = new GraphServiceClient(provider);

      var message = new Message
      {
        Body = new ItemBody
        {
          ContentType = BodyType.Text,
          Content = "Some content to send"
        },
        ToRecipients = new Recipient[] {
                    new Recipient { EmailAddress = new EmailAddress { Name = "Matt Lynam", Address = "mlynam@iteamnm.com" } },
                    new Recipient { EmailAddress = new EmailAddress { Name = "Alex Mayer", Address = "amayer@iteamnm.com" } },
                },
        Subject = "Test email",
      };

      client
          .Users["fce448c4-b650-45be-ac17-f7f36291ea16"]
          .SendMail(message, false)
          .Request()
          .PostAsync()
          .Wait();
    }
  }
}
