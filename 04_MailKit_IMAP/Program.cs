using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using MimeKit;
using System.Text;
using MailKit.Search;
using MailKit;

namespace _04_MailKit_IMAP
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string username = "lenailyshun@gmail.com";
            string password = "wilclwhglrlufffu";
            #region Send mail(SMTP)
            /*
           

            MimeMessage  message = new MimeMessage();
            message.From.Add(new MailboxAddress("Olena", "lenailyshun@gmail.com"));
            message.To.Add(new MailboxAddress("Test User", "rihaci7175@mugstock.com"));
            message.Subject = "Доброго вечора! Я з України!";
            message.Importance = MessageImportance.High;

            message.Body = new TextPart("plain")
            {
                Text = @" Привіт, Аліна!
                     Що ти плануєш робити на вихідних? 
                     Є пропозиція 
                     поїхати в аквапарк!
                "
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);

                client.Authenticate(username, password);

                client.Send(message);
                client.Disconnect(true);
            }
            */
            #endregion
            #region Get Mail Message (IMAP)
            Console.OutputEncoding = Encoding.UTF8;

            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, 
                    MailKit.Security.SecureSocketOptions.SslOnConnect);

                client.Authenticate(username, password);

                //foreach (var item in client.GetFolders(client.PersonalNamespaces[0]))
                //{
                //    Console.WriteLine("Folder : " + item.Name);
                //}

                //Read all messages in Folder Inbox
                //client.Inbox?.Open(FolderAccess.ReadOnly);
                //var uids = client.Inbox?.Search(SearchQuery.All);

                //foreach (var id in uids)
                //{
                //    var m = client.Inbox?.GetMessage(id);
                //    Console.WriteLine(m?.Subject);
                //}

                //Delete message
                //client.Inbox.Open(FolderAccess.ReadWrite);  
                //client?.GetFolder(SpecialFolder.Sent)?.Open(FolderAccess.ReadWrite);
                var folder = client?.GetFolder(SpecialFolder.Sent);
                folder?.Open(FolderAccess.ReadWrite);

                UniqueId id = (UniqueId)folder?.Search(SearchQuery.All).LastOrDefault()!;

                var m = folder.GetMessage(id);
                Console.WriteLine(m?.Subject);
                //folder.AddFlags(id, MessageFlags.Deleted, true);
                //client.Inbox.Expunge();

                folder.MoveTo(id, client.GetFolder(SpecialFolder.Junk));



                //var folder = client.GetFolder(SpecialFolder.Sent);
                //folder.Open(FolderAccess.ReadOnly);

                //var id = folder.Search(SearchQuery.All);
                //var lastId = id.LastOrDefault();

                //if (lastId != UniqueId.Invalid)
                //{
                //    var m = folder.GetMessage(lastId);
                //    Console.WriteLine(m.Subject);
                //}
                //else
                //{
                //    Console.WriteLine("No messages found in the Sent folder.");
                //}

            }
            #endregion

        }
    }
}
