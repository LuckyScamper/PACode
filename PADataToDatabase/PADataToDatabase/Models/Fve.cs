using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PADataToDatabase.Models
{
    public class Fve
    {
        [Key]
        public int RowIndex { get; set; }

        public string RegistrationCode { get; set; }
        public string Title { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public char Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool VoterStatus { get; set; }
        public DateTime? StatusChangeDate { get; set; }
        public char Affiliation { get; set; }
        public string HouseNumber { get; set; }
        public string HouseNumberSuffix { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string MailAddress1 { get; set; }
        public string MailAddress2 { get; set; }
        public string MailCity { get; set; }
        public string MailState { get; set; }
        public string MailZip { get; set; }
        public DateTime? LastVoteDate { get; set; }
        public string PrecintCode { get; set; }
        public string PrecintSplitId { get; set; }
        public DateTime? DateLastChanged { get; set; }
        public string LegacySystemIdNumber { get; set; }

        public string VotingMethod { get; set; }
        public string VotingParty { get; set; }
    }
}
