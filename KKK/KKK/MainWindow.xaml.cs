using ModelDB;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace KKK
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NpgClass npgClass;
        LoadData load;

        public MainWindow()
        {
            InitializeComponent();

          
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            npgClass = new NpgClass();
            load = new LoadData(npgClass);
            npgClass.Open(@"Server=localhost;Port=5432;User Id=postgres;Password=159753;Database=Playground_for_computer_games");
            npgClass.RecordBindingListUsers = new BindingList<UsersClass>();
            npgClass.RecordBindingListGame = new BindingList<GameClass>();
            npgClass.RecordBindingListFeedBack = new BindingList<FeedbackClass>();
            npgClass.RecordBindingListPayment = new BindingList<PaymentClass>();
            npgClass.RecordBindingListPaymentDataUser = new BindingList<PaymentDataUserClass>();
            npgClass.RecordBindingListRating = new BindingList<RatingClass>();
            npgClass.RecordBindingListRequirement = new BindingList<RequirementClass>();
            load.User_load(npgClass.Connection, userInfoDataGrid);
            load.List_games_Load(npgClass.Connection, listGamesDataGrid1);
            load.Payment_load(npgClass.Connection, paymentDataGrid1);
            load.Payment_data_user_load(npgClass.Connection, user_data_paymentDataGrid1);
            load.Requirement_Load(npgClass.Connection, requirementDataGrid1);
            load.Rating_Load(npgClass.Connection, ratingDataGrid1);
            load.Feedback_Load(npgClass.Connection, feedbackDataGrid1);
        }

        private void AddUseInfo_Click(object sender, RoutedEventArgs e)
        {
            string command = $"INSERT INTO user_platform(first_name, second_name, mail, pass, roles) " +
                 $"VALUES (@firstName, @secondName, @mail, @password, @roles)";
            NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
            nCommand.Parameters.AddWithValue("firstName", fir_name.Text);
            nCommand.Parameters.AddWithValue("secondName", sec_name.Text);
            nCommand.Parameters.AddWithValue("mail", mails.Text);
            nCommand.Parameters.AddWithValue("password", pasword.Text);
            nCommand.Parameters.AddWithValue("roles", rol.Text);
            nCommand.ExecuteNonQuery();

            load.User_load(npgClass.Connection, userInfoDataGrid);
        }

        private void DeleteUseInfo_Click(object sender, RoutedEventArgs e)
        {
            if (delete_user != null &&
            MessageBox.Show("Видалити запис?", "Повідомлення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"DELETE FROM user_platform WHERE user_id = @id";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("id", Convert.ToInt32(delete_user.Text));
                nCommand.ExecuteNonQuery();
            }
            load.User_load(npgClass.Connection, userInfoDataGrid);
        }

        private void AddUserDataPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string command = $"INSERT INTO payment_data_user(user_id, number_card, name_user, type_card, date_card, postal_code) " +
                 $"VALUES (@user_id, @number_card, @name_user, @type_card, @date_card, @postal_code)";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("user_id", Convert.ToInt32(us_id.Text));
                nCommand.Parameters.AddWithValue("number_card", numb_card.Text); //name_user
                nCommand.Parameters.AddWithValue("name_user", n_us.Text);
                nCommand.Parameters.AddWithValue("type_card", typ_card.Text);
                nCommand.Parameters.AddWithValue("date_card", Convert.ToDateTime(dt_card.Text));
                nCommand.Parameters.AddWithValue("postal_code", Convert.ToInt64(pos_cod.Text));
                nCommand.ExecuteNonQuery();

                load.Payment_data_user_load(npgClass.Connection, user_data_paymentDataGrid1);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteUserDataPayment_Click(object sender, RoutedEventArgs e)
        {
            if (delete_user_data_payment != null &&
            MessageBox.Show("Видалити запис?", "Повідомлення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"DELETE FROM payment_data_user WHERE data_id = @id";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("id", Convert.ToInt32(delete_user_data_payment.Text));
                nCommand.ExecuteNonQuery();
            }
            load.Payment_data_user_load(npgClass.Connection, user_data_paymentDataGrid1);
        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string command = $"INSERT INTO payment(payment_date, status, user_id, game_id, costt, payment_method) " +
                 $"VALUES (@payment_date, @status, @user_id, @game_id, @costt, @payment_method)";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("payment_date", Convert.ToDateTime(pay_date.Text));
                nCommand.Parameters.AddWithValue("status", statuus.Text); //name_user
                nCommand.Parameters.AddWithValue("user_id", Convert.ToInt64(user_id_in_payment.Text));
                nCommand.Parameters.AddWithValue("game_id", Convert.ToInt64(game_id_in_payment.Text));
                nCommand.Parameters.AddWithValue("costt", Convert.ToDouble(cost_payment.Text));
                nCommand.Parameters.AddWithValue("payment_method", pay_method.Text);
                nCommand.ExecuteNonQuery();

                load.Payment_load(npgClass.Connection, paymentDataGrid1);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeletePayment_Click(object sender, RoutedEventArgs e)
        {

            if (delete_payment != null &&
            MessageBox.Show("Видалити запис?", "Повідомлення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"DELETE FROM payment WHERE payment_id = @id";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("id", Convert.ToInt32(delete_payment.Text));
                nCommand.ExecuteNonQuery();
            }
            load.Payment_load(npgClass.Connection, paymentDataGrid1);

        }

        private void AddListGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string command = $"INSERT INTO game(game_name, company_developer, company_publisher, genre, description, release_year, platform, price) " +
                 $"VALUES (@game_name, @company_developer, @company_publisher, @genre, @description, @release_year, @platform, @price)";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("game_name", game_name_in_game.Text);
                nCommand.Parameters.AddWithValue("company_developer", company_develop_in_game.Text); //name_user
                nCommand.Parameters.AddWithValue("company_publisher", compan_publish_in_game.Text);
                nCommand.Parameters.AddWithValue("genre", genre_in_game.Text);
                nCommand.Parameters.AddWithValue("description", descrip_in_game.Text);
                nCommand.Parameters.AddWithValue("release_year", Convert.ToInt64(release_year_in_game.Text));
                nCommand.Parameters.AddWithValue("platform", platform_in_game.Text);
                nCommand.Parameters.AddWithValue("price", Convert.ToDouble(price_in_game.Text));
                nCommand.ExecuteNonQuery();

                load.List_games_Load(npgClass.Connection, listGamesDataGrid1);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } //VIDGYK 1

        private void DeleteListGame_Click(object sender, RoutedEventArgs e)
        {
            if (delete_game != null &&
            MessageBox.Show("Видалити запис?", "Повідомлення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"DELETE FROM game WHERE game_id = @id";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("id", Convert.ToInt32(delete_game.Text));
                nCommand.ExecuteNonQuery();
            }
            load.List_games_Load(npgClass.Connection, listGamesDataGrid1);
        }  //VIDGYK 2

        private void SortReleaseGame_Click(object sender, RoutedEventArgs e)
        {
            npgClass.RecordBindingListGame.Clear();
            string command = "SELECT * FROM game ORDER BY release_year ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListGame.Add(new GameClass((int)reader["game_id"], reader["game_name"].ToString(), reader["company_developer"].ToString(), reader["company_publisher"].ToString(), reader["genre"].ToString(), reader["description"].ToString(), (int)reader["release_year"], reader["platform"].ToString(), double.Parse(Convert.ToDouble(reader["price"]).ToString("F2"))));
            }

            reader.Close();

            listGamesDataGrid1.ItemsSource = npgClass.RecordBindingListGame;
            foreach (DataGridColumn column in listGamesDataGrid1.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }  //VIDGYK 3

        private void ViewGameGenre_Click(object sender, RoutedEventArgs e) 
        {

            if (genre_search != null)
            {
                npgClass.RecordBindingListGame.Clear();
                

                string command = $"SELECT * FROM game WHERE genre='{genre_search.Text}'";

                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);

                NpgsqlDataReader reader = nCommand.ExecuteReader();

                while (reader.Read())
                {
                    npgClass.RecordBindingListGame.Add(new GameClass((int)reader["game_id"], reader["game_name"].ToString(), reader["company_developer"].ToString(), reader["company_publisher"].ToString(), reader["genre"].ToString(), reader["description"].ToString(), (int)reader["release_year"], reader["platform"].ToString(), double.Parse(Convert.ToDouble(reader["price"]).ToString("F2"))));
                }

                reader.Close();

                listGamesDataGrid1.ItemsSource = npgClass.RecordBindingListGame;
                foreach (DataGridColumn column in listGamesDataGrid1.Columns)
                {
                    column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }
            else
            {
                MessageBox.Show($"Ви не обрали жанр гри!!!" , "Увага!!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }  //VIDGYK 4

        private void UpdateTableGame_Click(object sender, RoutedEventArgs e)  
        {
            load.List_games_Load(npgClass.Connection, listGamesDataGrid1);
        }      //VIDGYK 5

        private void AddRequirement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string command = $"INSERT INTO requirement(game_id, operating_system, processor, ram, video_card, disk_space) " +
                 $"VALUES (@game_id, @operating_system, @processor, @ram, @video_card, @disk_space)";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("game_id", Convert.ToInt64(game_id_in_requirement.Text));
                nCommand.Parameters.AddWithValue("operating_system", oper_system_in_requirement.Text); //name_user
                nCommand.Parameters.AddWithValue("processor", processor_in_requirement.Text);
                nCommand.Parameters.AddWithValue("ram", ram_in_requirement.Text);
                nCommand.Parameters.AddWithValue("video_card", video_card_in_requirement.Text);
                nCommand.Parameters.AddWithValue("disk_space", disk_space_in_requirement.Text);
                nCommand.ExecuteNonQuery();

                load.Requirement_Load(npgClass.Connection, requirementDataGrid1);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteRequirement_Click(object sender, RoutedEventArgs e)
        {
            if (delete_requirement != null &&
            MessageBox.Show("Видалити запис?", "Повідомлення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"DELETE FROM requirement WHERE game_id = @id";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("id", Convert.ToInt32(delete_requirement.Text));
                nCommand.ExecuteNonQuery();
            }
            load.Requirement_Load(npgClass.Connection, requirementDataGrid1);
        }

        private void AddRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string command = $"INSERT INTO rating(game_id, rating_number, number_of_evaluations) " +
                 $"VALUES (@game_id, @rating_number, @number_of_evaluations)";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("game_id", Convert.ToInt64(game_id_in_rating.Text));
                nCommand.Parameters.AddWithValue("rating_number", Convert.ToDouble(rating_number_in_rating.Text)); //name_user
                nCommand.Parameters.AddWithValue("number_of_evaluations", Convert.ToInt64(number_of_evalution_in_rating.Text));
                nCommand.ExecuteNonQuery();

                load.Rating_Load(npgClass.Connection, ratingDataGrid1);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteRating_Click(object sender, RoutedEventArgs e)
        {
            if (delete_rating != null &&
            MessageBox.Show("Видалити запис?", "Повідомлення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"DELETE FROM rating WHERE game_id = @id";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("id", Convert.ToInt32(delete_rating.Text));
                nCommand.ExecuteNonQuery();
            }
            load.Rating_Load(npgClass.Connection, ratingDataGrid1);
        }

        private void AddFeedback_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string command = $"INSERT INTO feedback(user_id, game_id, date_review, fb_text, evaluation) " +
                 $"VALUES @user_id, @game_id, @date_review, @fb_text, @evaluation)";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("user_id", Convert.ToInt64(user_id_in_feedback.Text));
                nCommand.Parameters.AddWithValue("game_id", Convert.ToInt64(game_id_in_feedback.Text));
                nCommand.Parameters.AddWithValue("date_review", Convert.ToDateTime(date_review_in_feedback.Text));
                nCommand.Parameters.AddWithValue("fb_text", feedback_in_feedback.Text);
                nCommand.Parameters.AddWithValue("evaluations", Convert.ToInt64(evaluation_in_feedback.Text));
                nCommand.ExecuteNonQuery();

                load.Feedback_Load(npgClass.Connection, feedbackDataGrid1);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (delete_feedback != null &&
            MessageBox.Show("Видалити запис?", "Повідомлення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"DELETE FROM feedback WHERE user_id = @id";
                NpgsqlCommand nCommand = new NpgsqlCommand(command, npgClass.Connection);
                nCommand.Parameters.AddWithValue("id", Convert.ToInt32(delete_feedback.Text));
                nCommand.ExecuteNonQuery();
            }
            load.Feedback_Load(npgClass.Connection, feedbackDataGrid1);
        }

    }

    internal class LoadData
    {
        NpgClass npgClass;
        public LoadData(NpgClass _class)
        {
            npgClass = _class;
        }
        public void User_load(NpgsqlConnection Connection, DataGrid userInfoDataGrid)
        {
            npgClass.RecordBindingListUsers.Clear();
            string command = "SELECT * FROM user_platform ORDER BY user_id ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListUsers.Add(new UsersClass((int)reader["user_id"], reader["first_name"].ToString(), reader["second_name"].ToString(), reader["mail"].ToString(), reader["pass"].ToString(), reader["roles"].ToString()));
            }

            reader.Close();

            userInfoDataGrid.ItemsSource = npgClass.RecordBindingListUsers;
            foreach (DataGridColumn column in userInfoDataGrid.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void Payment_data_user_load(NpgsqlConnection Connection, DataGrid user_data_paymentDataGrid1)
        {
            npgClass.RecordBindingListPaymentDataUser.Clear();
            string command = "SELECT * FROM payment_data_user ORDER BY data_id ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListPaymentDataUser.Add(new PaymentDataUserClass((int)reader["data_id"], (int)reader["user_id"], reader["number_card"].ToString(), reader["name_user"].ToString(), reader["type_card"].ToString(), (DateTime)reader["date_card"], (int)reader["postal_code"]));
            }

            reader.Close();

            user_data_paymentDataGrid1.ItemsSource = npgClass.RecordBindingListPaymentDataUser;
            foreach (DataGridColumn column in user_data_paymentDataGrid1.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void Payment_load(NpgsqlConnection Connection, DataGrid paymentDataGrid1)
        {
            npgClass.RecordBindingListPayment.Clear();
            string command = "SELECT * FROM payment ORDER BY payment_id ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListPayment.Add(new PaymentClass((int)reader["payment_id"], (DateTime)reader["payment_date"], reader["status"].ToString(), (int)reader["user_id"], (int)reader["game_id"], double.Parse(Convert.ToDouble(reader["costt"]).ToString("F2")), reader["payment_method"].ToString()));
            }

            reader.Close();

            paymentDataGrid1.ItemsSource = npgClass.RecordBindingListPayment;
            foreach (DataGridColumn column in paymentDataGrid1.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void List_games_Load(NpgsqlConnection Connection, DataGrid listGamesDataGrid1)
        {
            npgClass.RecordBindingListGame.Clear();
            string command = "SELECT * FROM game ORDER BY game_id ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListGame.Add(new GameClass((int)reader["game_id"], reader["game_name"].ToString(), reader["company_developer"].ToString(), reader["company_publisher"].ToString(), reader["genre"].ToString(), reader["description"].ToString(), (int)reader["release_year"], reader["platform"].ToString(), double.Parse(Convert.ToDouble(reader["price"]).ToString("F2"))));
            }

            reader.Close();

            listGamesDataGrid1.ItemsSource = npgClass.RecordBindingListGame;
            foreach (DataGridColumn column in listGamesDataGrid1.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }


        }

        public void Requirement_Load(NpgsqlConnection Connection, DataGrid requirementDataGrid1)
        {
            npgClass.RecordBindingListRequirement.Clear();
            string command = "SELECT * FROM requirement ORDER BY game_id ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListRequirement.Add(new RequirementClass((int)reader["game_id"], reader["operating_system"].ToString(), reader["processor"].ToString(), reader["ram"].ToString(), reader["video_card"].ToString(), reader["disk_space"].ToString()));
            }

            reader.Close();

            requirementDataGrid1.ItemsSource = npgClass.RecordBindingListRequirement;
            foreach (DataGridColumn column in requirementDataGrid1.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }

        }

        public void Rating_Load(NpgsqlConnection Connection, DataGrid ratingDataGrid1)
        {
            npgClass.RecordBindingListRating.Clear();
            string command = "SELECT * FROM rating ORDER BY game_id ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListRating.Add(new RatingClass((int)reader["game_id"], Convert.ToDouble(reader["rating_number"]), (int)reader["number_of_evaluations"]));
            }

            reader.Close();

            ratingDataGrid1.ItemsSource = npgClass.RecordBindingListRating;
            foreach (DataGridColumn column in ratingDataGrid1.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void Feedback_Load(NpgsqlConnection Connection, DataGrid feedbackDataGrid1)
        {
            npgClass.RecordBindingListFeedBack.Clear();
            string command = "SELECT * FROM feedback ORDER BY user_id ASC";

            NpgsqlCommand nCommand = new NpgsqlCommand(command, Connection);

            NpgsqlDataReader reader = nCommand.ExecuteReader();

            while (reader.Read())
            {
                npgClass.RecordBindingListFeedBack.Add(new FeedbackClass((int)reader["user_id"], (int)reader["game_id"], (DateTime)reader["date_review"], reader["fb_text"].ToString(), (int)reader["evaluation"]));
            }

            reader.Close();

            feedbackDataGrid1.ItemsSource = npgClass.RecordBindingListFeedBack;
            foreach (DataGridColumn column in feedbackDataGrid1.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

    }


}