namespace Security.Mailing.MailData
{
    using System;

    [Serializable]
    public class VerificationEmailData 
    {
        public VerificationEmailData(Guid userId, string verificationToken)
        {
            UserId = userId;
            VerificationToken = verificationToken;
        }

        public Guid UserId { get; set; }

        public string VerificationToken { get; set; }
    }
}
