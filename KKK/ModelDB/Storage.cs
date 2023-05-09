using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows;
using System.Runtime.CompilerServices;

namespace ModelDB
{
   
    public class UsersClass
    {
        public UsersClass(int _user_id, string _first_name, string _second_name, string _mail, string _pass, string _roles)
        {
            User_id = _user_id;
            First_name = _first_name;
            Second_name = _second_name;
            Mail = _mail;
            Pass = _pass;
            Roles = _roles;
        }

        public int User_id { get; set; }
        public string First_name { get; set; }
        public string Second_name { get; set; }
        public string Mail { get; set; }
        public string Pass { get; set; }
        public string Roles { get; set; }
    }

    public class GameClass
    {
        public GameClass(int _game_id, string _game_name, string _company_developer, string _company_publisher, string _genre, string _descriptio, int _release_year, string _platform, double price)
        {
            Game_id = _game_id;
            Game_name = _game_name;
            Company_developer = _company_developer;
            Company_publisher = _company_publisher;
            Genre = _genre;
            Description = _descriptio;
            Release_year = _release_year;
            Platform = _platform;
            Price = price;
        }

        public int Game_id { get; set; }
        public string Game_name { get; set; }
        public string Company_developer { get; set; }
        public string Company_publisher { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Release_year { get; set; }
        public string Platform { get; set; }
        public double Price { get; set; }
    }

    public class PaymentDataUserClass
    {
        public PaymentDataUserClass(int _data_id, int _user_id, string _number_card, string _name_user, string _type_card, DateTime _date_card, int _postal_code)
        {
            Data_id = _data_id;
            User_id = _user_id;
            Number_card = _number_card;
            Name_user = _name_user;
            Type_card = _type_card;
            Date_card = _date_card;
            Postal_code = _postal_code;
        }

        public int Data_id { get; set; }
        public int User_id { get; set; }
        public string Number_card { get; set; }
        public string Name_user { get; set; }
        public string Type_card { get; set; }
        public DateTime Date_card { get; set; }
        public int Postal_code { get; set; }
    }

    public class PaymentClass
    {
        public PaymentClass(int _payment_id, DateTime _payment_date, string _status, int _user_id, int _game_id, double _cost, string _payment_method)
        {
            Payment_id = _payment_id;
            Payment_date = _payment_date;
            Status = _status;
            User_id = _user_id;
            Game_id = _game_id;
            Cost = _cost;
            Payment_method = _payment_method;

        }

        public int Payment_id { get; set; }
        public DateTime Payment_date { get; set; }
        public string Status { get; set; }
        public int User_id { get; set; }
        public int Game_id { get; set; }
        public double Cost { get; set; }
        public string Payment_method { get; set; }
    }

    public class RequirementClass
    {
        public RequirementClass(int _game_id, string _operating_system, string _processor, string _ram, string _video_card, string _disk_space)
        {
            Game_id = _game_id;
            Operating_system = _operating_system;
            Processor = _processor;
            Ram = _ram;
            Video_card = _video_card;
            Disk_space = _disk_space;
        }

        public int Game_id { get; set; }
        public string Operating_system { get; set; }
        public string Processor { get; set; }
        public string Ram { get; set; }
        public string Video_card { get; set; }
        public string Disk_space { get; set; }
    }

    public class FeedbackClass
    {
        public FeedbackClass(int _user_id, int _game_id, DateTime _date_rewie, string _fb_text, int _evaluation)
        {
            User_id = _user_id;
            Game_id = _game_id;
            Date_rewie = _date_rewie;
            Fb_text = _fb_text;
            Evaluation = _evaluation;
        }

        public int User_id { get; set; }
        public int Game_id { get; set; }
        public DateTime Date_rewie { get; set; }
        public string Fb_text { get; set; }
        public int Evaluation { get; set; }
    }

    public class RatingClass
    {
        public RatingClass(int _game_id, double _rating_number, int _number_of_evaluations)
        {
            Game_id = _game_id;
            Rating_number = _rating_number;
            Number_of_evaluations = _number_of_evaluations;
        }

        public int Game_id { get; set; }
        public double Rating_number { get; set; }
        public int Number_of_evaluations { get; set; }

    }

    public class NpgClass
    {
        public NpgsqlConnection Connection { get; set; }

        public void Open(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }
        #region BindingList_Records
        public BindingList<UsersClass> RecordBindingListUsers { get; set; }
        public BindingList<GameClass> RecordBindingListGame { get; set; }
        public BindingList<PaymentClass> RecordBindingListPayment { get; set; }
        public BindingList<PaymentDataUserClass> RecordBindingListPaymentDataUser { get; set; }
        public BindingList<FeedbackClass> RecordBindingListFeedBack { get; set; }
        public BindingList<RatingClass> RecordBindingListRating { get; set; }
        public BindingList<RequirementClass> RecordBindingListRequirement { get; set; }
        #endregion
    }

}
