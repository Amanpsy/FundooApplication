using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messagequeue = new MessageQueue();
        public void sendData2Queue(string token)
        {

            messagequeue.Path = @".\private$\Token";
            if (!MessageQueue.Exists(messagequeue.Path))
            {
                MessageQueue.Create(messagequeue.Path);
            }



            messagequeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messagequeue.ReceiveCompleted += MessageQueue_ReceiveCompleted; ;  //+= means Event
            messagequeue.Send(token);
            messagequeue.BeginReceive();
            messagequeue.Close();

        }



        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {

                var msg = messagequeue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "Fundoo Note Reset link";
                string body = token;
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("amankrbridgelabz@gmail.com", "zrmijgwgjgshstje"),
                    EnableSsl = true,



                };

                smtp.Send("amankrbridgelabz@gmail.com", "amankrbridgelabz@gmail.com", subject, body);

                messagequeue.BeginReceive();
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
       
    }

