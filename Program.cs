using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapYönetimSistemiYeniVersiyon
{
    internal class Program
    {
        static void Main(string[] args)
        {
           string connectionString = "Data Source=DESKTOP-9EJ59KT;Initial Catalog=BookListing;Integrated Security=True";

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                Console.Write("İşlemler \n1-Ekle\n2-Güncelle\n3-Sil\n4-Listele : \n");
                int secim = Convert.ToInt32(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        VeriEkle(connection);
                        break;
                    case 2:
                        VeriGuncelle(connection);
                        break;
                    case 3:
                        VeriSil(connection);
                        break;
                    case 4:
                        VeriListele(connection);
                        break;
                    default:
                        Console.WriteLine("404 NOT PAGE FOUND");
                        break;
                }
                connection.Close();
            }
        }

        static void VeriEkle(SqlConnection connection)
        {
            Console.Write("Yeni kitap Girişi : ");
            string yeniKitap = Console.ReadLine();

            Console.Write("Tür : ");
            string yeniKitapTur = Console.ReadLine();

            Console.Write("Kategori : ");
            string yeniKitapKategori = Console.ReadLine();

            Console.Write("Yazar : ");
            string yeniKitapYazar = Console.ReadLine();

            string query = "INSERT INTO Tbl_Kitaplar (KitapAd,KitapTür,KitapKategori,KitapYazar) VALUES (@kitapAd,@kitapTur,@kitapKategori,@kitapYazar)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@kitapAd",yeniKitap);
                command.Parameters.AddWithValue("@kitapTur", yeniKitapTur);
                command.Parameters.AddWithValue("@kitapKategori", yeniKitapKategori);
                command.Parameters.AddWithValue("@kitapYazar", yeniKitapYazar);
                command.ExecuteNonQuery();
                Console.WriteLine("Kitap Veri Tabanına Eklenmiştir");
            }
        }

        static void VeriGuncelle(SqlConnection connection)
        {
            Console.Write("Güncellenecek ID'yi Giriniz : ");
            int updateID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Kitap Adı : ");
            string updateBook = Console.ReadLine();

            Console.Write("Kitap Tür : ");
            string updateBookType = Console.ReadLine();

            Console.Write("Kitap Kategorisi : ");
            string updateBookCategory = Console.ReadLine();

            Console.Write("Kitap Yazar : ");
            string updateBookWriter = Console.ReadLine();

            string query = "UPDATE Tbl_Kitaplar SET KitapAd=@kitapAd,KitapTür=@kitapTur,KitapKategori=@kitapKategori,KitapYazar=@kitapYazar WHERE KitapID=@kitapId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@kitapId", updateID);
                command.Parameters.AddWithValue("@kitapAd", updateBook);
                command.Parameters.AddWithValue("@kitapTur", updateBookType);
                command.Parameters.AddWithValue("@kitapKategori", updateBookCategory);
                command.Parameters.AddWithValue("@kitapYazar", updateBookWriter);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} satır güncellendi");
            }
        }
        
        static void VeriSil(SqlConnection connection)
        {
            Console.Write("Kaldıracağınız Kitapın ID'sini giriniz : ");
            int deleteID = Convert.ToInt32(Console.ReadLine());

            string queryDelete = "DELETE FROM Tbl_Kitaplar WHERE KitapID=@kitapId";
            using (SqlCommand command = new SqlCommand(queryDelete, connection))
            {
                command.Parameters.AddWithValue("@kitapId",deleteID);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} satır silindi");
            }
        }

        static void VeriListele(SqlConnection connection)
        {
            string showAllData = "SELECT * FROM Tbl_Kitaplar";
            using (SqlCommand books = new SqlCommand(showAllData, connection))
            {
                using (SqlDataReader reader = books.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["KitapID"]} \n{reader["KitapAd"]} \n{reader["KitapTür"]} \n{reader["KitapKategori"]} \n{reader["KitapYazar"]}");
                    }
                }
            }
            Console.Read();
        }
    }
}
