using LocationVoitureWPF.classeMetier;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.coucheAccesBD
{
    public class AccesBD
    {
        private NpgsqlConnection SqlConn;
        /// <summary>
        /// Constructeur : créer une instance de la classe AccesBD
        /// </summary>
        public AccesBD()
        {
            try
            {

                this.SqlConn = new NpgsqlConnection("Server=localhost;" +
                "port=5432;" +
                "Database=LocationVoiture_DB;" +
                "UserID=postgres;" +
                "Password = 72hoignee");


                this.SqlConn.Open();
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion à la BD", e.Message);
            }
        }

        #region CLIENT
        /// <summary>
        /// Liste des clients
        /// </summary>
        /// <returns></returns>
        public List<Client> ListClients()
        {
            List<Client> liste = null;
            NpgsqlCommand sqlCmd = null;

            try
            {
                sqlCmd = new NpgsqlCommand("SELECT Id, Nom, Prenom, DateNaissance, NumPermisConduire, " +
                        "Pays, Region, Ville, Adresse, Cp  FROM listeClients()", this.SqlConn);


                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    liste = new List<Client>();

                    do
                    {
                        liste.Add(new Client(Convert.ToInt32(sqlreader["Id"]),
                                                Convert.ToString(sqlreader["Nom"]),
                                                Convert.ToString(sqlreader["Prenom"]),
                                                Convert.ToDateTime(sqlreader["DateNaissance"]),
                                                Convert.ToString(sqlreader["NumPermisConduire"]),
                                                Convert.ToString(sqlreader["Pays"]),
                                                Convert.ToString(sqlreader["Region"]),
                                                Convert.ToString(sqlreader["Ville"]),
                                                Convert.ToString(sqlreader["Adresse"]),
                                                Convert.ToInt32(sqlreader["Cp"])));
                    } while (sqlreader.Read());

                }
                sqlreader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }

            return liste;
        }

        /// <summary>
        /// Ajoute une entrée dans la table CLIENT.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public int AjouterClient(Client client)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM AjouterClient"
                                           + " (:nom, :prenom, :dateNaissance, :numPermisConduire, :pays, :region, :ville,  :adresse, :cp)", this.SqlConn);

                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("nom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("prenom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("dateNaissance",
                    NpgsqlTypes.NpgsqlDbType.Date));
                sqlCmd.Parameters.Add(new NpgsqlParameter("numPermisConduire",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("pays",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("region",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("ville",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("adresse",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("cp",
                    NpgsqlTypes.NpgsqlDbType.Integer));


                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = client.Nom;
                sqlCmd.Parameters[1].Value = client.Prenom;
                sqlCmd.Parameters[2].Value = client.DateNaissance;
                sqlCmd.Parameters[3].Value = client.NumPermisConduire;
                sqlCmd.Parameters[4].Value = client.Pays;
                sqlCmd.Parameters[5].Value = client.Region;
                sqlCmd.Parameters[6].Value = client.Ville;
                sqlCmd.Parameters[7].Value = client.Adresse;
                sqlCmd.Parameters[8].Value = client.Cp;

                // Execute la commande

                return ((int)sqlCmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Obtient une entrée de la table CLIENT en fonction de son numero de permis de conduire
        /// </summary>
        /// <param name="numPermisConduire"></param>
        /// <returns></returns>
        public Client GetClient(string numPermisConduire)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Client client;
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetClient(:numPermisConduire)", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("numPermisConduire",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = numPermisConduire;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    client = new Client(sqlreader["Id"] == DBNull.Value ? -1 : Convert.ToInt32(sqlreader["Id"]),
                                                sqlreader["Nom"] == DBNull.Value ? null : Convert.ToString(sqlreader["Nom"]),
                                                sqlreader["Prenom"] == DBNull.Value ? null : Convert.ToString(sqlreader["Prenom"]),
                                                sqlreader["DateNaissance"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(sqlreader["DateNaissance"]),
                                                sqlreader["NumPermisConduire"] == DBNull.Value ? null : Convert.ToString(sqlreader["NumPermisConduire"]),
                                                sqlreader["Pays"] == DBNull.Value ? null : Convert.ToString(sqlreader["Pays"]),
                                                sqlreader["Region"] == DBNull.Value ? null : Convert.ToString(sqlreader["Region"]),
                                                sqlreader["Ville"] == DBNull.Value ? null : Convert.ToString(sqlreader["Ville"]),
                                                sqlreader["Adresse"] == DBNull.Value ? null : Convert.ToString(sqlreader["Adresse"]),
                                                sqlreader["Cp"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["Cp"]));

                    sqlreader.Close();
                    return client;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Obtient une entrée de la table CLIENT en fonction de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client GetClient(int id)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Client client;
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetClientById(:id)", this.SqlConn);

                sqlCmd.Parameters.Add(new NpgsqlParameter("id",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = id;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    client = new Client(Convert.ToInt32(sqlreader["Id"]),
                                                Convert.ToString(sqlreader["Nom"]),
                                                Convert.ToString(sqlreader["Prenom"]),
                                                Convert.ToDateTime(sqlreader["DateNaissance"]),
                                                Convert.ToString(sqlreader["NumPermisConduire"]),
                                                Convert.ToString(sqlreader["Pays"]),
                                                Convert.ToString(sqlreader["Region"]),
                                                Convert.ToString(sqlreader["Ville"]),
                                                Convert.ToString(sqlreader["Adresse"]),
                                                Convert.ToInt32(sqlreader["Cp"]));

                    sqlreader.Close();
                    return client;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Met à jour une entrée dans la table CLIENT en fonction de son identifiant.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public int UpdateClient(Client client)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM  UpdateClient(:id, :nom, :prenom, :dateNaissance, :numPermisConduire, :pays, :region, :ville, :adresse, :cp)" , this.SqlConn);

                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("nom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("prenom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("dateNaissance",
                    NpgsqlTypes.NpgsqlDbType.Date));
                sqlCmd.Parameters.Add(new NpgsqlParameter("numPermisConduire",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("pays",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("region",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("ville",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("adresse",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("cp",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Parameters.Add(new NpgsqlParameter("id",
                    NpgsqlTypes.NpgsqlDbType.Integer));


                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = client.Nom;
                sqlCmd.Parameters[1].Value = client.Prenom;
                sqlCmd.Parameters[2].Value = client.DateNaissance;
                sqlCmd.Parameters[3].Value = client.NumPermisConduire;
                sqlCmd.Parameters[4].Value = client.Pays;
                sqlCmd.Parameters[5].Value = client.Region;
                sqlCmd.Parameters[6].Value = client.Ville;
                sqlCmd.Parameters[7].Value = client.Adresse;
                sqlCmd.Parameters[8].Value = client.Cp;
                sqlCmd.Parameters[9].Value = client.Id;

                // Execute la commande

                return ((int)sqlCmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }
        #endregion

        #region MODELE
        /// <summary>
        /// Liste des voitures
        /// </summary>
        /// <returns></returns>
        public List<Modele> ListModeles()
        {
            List<Modele> liste = null;
            NpgsqlCommand sqlCmd = null;

            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM ListModeles()", this.SqlConn);


                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    liste = new List<Modele>();

                    do
                    {
                        liste.Add(new Modele(Convert.ToInt32(sqlreader["Id"]),
                                                Convert.ToString(sqlreader["CategorieNom"]),
                                                Convert.ToString(sqlreader["Marque"]),
                                                Convert.ToString(sqlreader["Nom"]),
                                                Convert.ToInt32(sqlreader["NbrSieges"])));
                    } while (sqlreader.Read());

                }
                sqlreader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }

            return liste;
        }

        /// <summary>
        /// Obtient une entrée de la table MODELE en fonction de l'id du modèle.
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <returns></returns>
        public Modele GetModele(int? id)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Modele modele = new Modele();
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetModele(:id)", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("id",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = id;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    modele.Id = Convert.ToInt32(sqlreader["Id"]);
                    modele.CategorieNom = Convert.ToString(sqlreader["CategorieNom"]);
                    modele.Marque = Convert.ToString(sqlreader["Marque"]);
                    modele.NbrSieges = Convert.ToInt32(sqlreader["NbrSieges"]);
                    modele.Nom = Convert.ToString(sqlreader["Nom"]);
                    sqlreader.Close();
                    return modele;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Vérifie si un modèle existe déjà en BD.
        /// </summary>
        /// <param name="modele"></param>
        /// <returns></returns>
        public Modele CheckModele(Modele modele)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM CheckModele(:marque, :nom, :nbrSieges, :categorieNom)", this.SqlConn);

                sqlCmd.Parameters.Add(new NpgsqlParameter("marque",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("nom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("nbrSieges",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Parameters.Add(new NpgsqlParameter("categorieNom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = modele.Marque;
                sqlCmd.Parameters[1].Value = modele.Nom;
                sqlCmd.Parameters[2].Value = modele.NbrSieges;
                sqlCmd.Parameters[3].Value = modele.CategorieNom;


                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    Modele modeleChecker = new Modele();
                    modeleChecker.Id = sqlreader["Id"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["Id"]);
                    modeleChecker.CategorieNom = sqlreader["CategorieNom"] == DBNull.Value ? null : Convert.ToString(sqlreader["CategorieNom"]);
                    modeleChecker.Marque = sqlreader["Marque"] == DBNull.Value ? null : Convert.ToString(sqlreader["Marque"]);
                    modeleChecker.NbrSieges = sqlreader["NbrSieges"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["NbrSieges"]);
                    modeleChecker.Nom = sqlreader["Nom"] == DBNull.Value ? null : Convert.ToString(sqlreader["Nom"]);
                    sqlreader.Close();
                    return modeleChecker;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Ajoute une entrée dans la table MODELE.
        /// </summary>
        /// <param name="modele"></param>
        /// <returns></returns>
        public int AjouterModele(Modele modele)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                //sqlCmd = new NpgsqlCommand("INSERT INTO \"MODELE\" (Marque, Nom, NbrSieges, CategorieNom) "+
                //                            "VALUES(:marque, :nom, :nbrSieges, :categorieNom); ", this.SqlConn);

                sqlCmd = new NpgsqlCommand("SELECT * FROM AjouterModele(:marque, :nom, :nbrSieges, :categorieNom);", this.SqlConn);

                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("marque",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("nom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("nbrSieges",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Parameters.Add(new NpgsqlParameter("categorieNom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));


                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = modele.Marque;
                sqlCmd.Parameters[1].Value = modele.Nom;
                sqlCmd.Parameters[2].Value = modele.NbrSieges;
                sqlCmd.Parameters[3].Value = modele.CategorieNom;

                // Execute la commande

                return ((int)sqlCmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }
        #endregion

        #region CATEGORIE
        /// <summary>
        /// Liste des catégories
        /// </summary>
        /// <returns></returns>
        public List<Categorie> ListCategories()
        {
            List<Categorie> liste = null;
            NpgsqlCommand sqlCmd = null;

            try
            {
                sqlCmd = new NpgsqlCommand("SELECT Nom, PrixJour FROM ListCategories()", this.SqlConn);


                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    liste = new List<Categorie>();

                    do
                    {
                        liste.Add(new Categorie(Convert.ToString(sqlreader["Nom"]),
                                                Convert.ToDecimal(sqlreader["PrixJour"])));
                    } while (sqlreader.Read());

                }
                sqlreader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }

            return liste;
        }

        /// <summary>
        /// Obtient une entrée de la table CATEGORIE en fonction de son nom ou categorieNom.
        /// </summary>
        /// <param name="categorieNom"></param>
        /// <returns></returns>
        public Categorie GetCategorie(string categorieNom)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Categorie categorie = new Categorie();
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetCategorie(:categorieNom)", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("categorieNom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = categorieNom;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    categorie.Nom = sqlreader["Nom"] == DBNull.Value ? null : Convert.ToString(sqlreader["Nom"]);
                    categorie.PrixJour = sqlreader["PrixJour"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(sqlreader["PrixJour"]);
                    sqlreader.Close();
                    return categorie;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Ajoute une entrée dans la table CETEGORIE.
        /// </summary>
        /// <param name="modele"></param>
        /// <returns></returns>
        public int AjouterCategorie(Categorie categorie)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM AjouterCategorie(:nom, :prixJour); ", this.SqlConn);

                // Ajoute les paramètres
                sqlCmd.Parameters.Add(new NpgsqlParameter("nom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("prixJour",
                    NpgsqlTypes.NpgsqlDbType.Numeric));

                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = categorie.Nom;
                sqlCmd.Parameters[1].Value = categorie.PrixJour;

                // Execute la commande

                return (sqlCmd.ExecuteNonQuery());
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }
        #endregion

        #region VOITURE
        /// <summary>
        /// Liste des voitures
        /// </summary>
        /// <returns></returns>
        public List<Voiture> ListVoitures()
        {
            List<Voiture> liste = null;
            NpgsqlCommand sqlCmd = null;

            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM ListVoitures();", this.SqlConn);


                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    liste = new List<Voiture>();

                    do
                    {
                        liste.Add(new Voiture(Convert.ToInt32(sqlreader["Id"]),
                                                Convert.ToString(sqlreader["Immatriculation"]),
                                                Convert.ToString(sqlreader["Couleur"]),
                                                Convert.ToInt32(sqlreader["ModeleId"])));
                    } while (sqlreader.Read());

                }
                sqlreader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }

            return liste;
        }

        /// <summary>
        /// Liste des voitures louées
        /// </summary>
        /// <returns></returns>
        public List<Voiture> ListVoituresLouees()
        {
            List<Voiture> liste = null;
            NpgsqlCommand sqlCmd = null;

            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM ListVoituresLouees();", this.SqlConn);

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    liste = new List<Voiture>();

                    do
                    {
                        liste.Add(new Voiture(Convert.ToInt32(sqlreader["Id"]),
                                                Convert.ToString(sqlreader["Immatriculation"]),
                                                Convert.ToString(sqlreader["Couleur"]),
                                                Convert.ToInt32(sqlreader["ModeleId"])));
                    } while (sqlreader.Read());

                }
                sqlreader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }

            return liste;
        }

        /// <summary>
        /// Liste des voitures disponibles
        /// </summary>
        /// <returns></returns>
        public List<Voiture> ListVoituresDisponibles()
        {
            List<Voiture> liste = null;
            NpgsqlCommand sqlCmd = null;

            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM ListVoituresDisponibles();", this.SqlConn);

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    liste = new List<Voiture>();

                    do
                    {
                        liste.Add(new Voiture(Convert.ToInt32(sqlreader["Id"]),
                                                Convert.ToString(sqlreader["Immatriculation"]),
                                                Convert.ToString(sqlreader["Couleur"]),
                                                Convert.ToInt32(sqlreader["ModeleId"])));
                    } while (sqlreader.Read());

                }
                sqlreader.Close();

            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }

            return liste;
        }

        /// <summary>
        /// // Ajouter une voiture
        /// </summary>
        /// <param name="categorie"></param>
        /// <returns></returns>
        public int AjouterVoiture(Voiture voiture)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM AjouterVoiture(:immatriculation, :couleur, :modeleId)", this.SqlConn);

                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("immatriculation",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("couleur",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("modeleId",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = voiture.Immatriculation;
                sqlCmd.Parameters[1].Value = voiture.Couleur;
                sqlCmd.Parameters[2].Value = voiture.ModeleId;

                // Execute la commande

                return ((int)sqlCmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Obtient une voiture en fonction du numéro d'immatriculation.
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <returns></returns>
        public Voiture GetVoiture(string immatriculation)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Voiture voiture = new Voiture();
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetVoiture(:immatriculation);", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("immatriculation",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = immatriculation;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    voiture.Id = sqlreader["Id"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["Id"]);
                    voiture.Immatriculation = sqlreader["Immatriculation"] == DBNull.Value ? null : Convert.ToString(sqlreader["Immatriculation"]);
                    voiture.ModeleId = sqlreader["ModeleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["ModeleId"]);
                    voiture.Couleur = sqlreader["Couleur"] == DBNull.Value ? null : Convert.ToString(sqlreader["Couleur"]);
                    sqlreader.Close();
                    return voiture;
                }
                else {
                    sqlreader.Close();
                    return null;
                } 
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Obtient une voiture en fonction du numéro d'immatriculation.
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <returns></returns>
        public Voiture GetVoiture(int voitureId)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Voiture voiture = new Voiture();
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetVoitureById(:voitureId)", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("voitureId",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = voitureId;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    voiture.Id = Convert.ToInt32(sqlreader["Id"]);
                    voiture.Immatriculation = Convert.ToString(sqlreader["Immatriculation"]);
                    voiture.ModeleId = Convert.ToInt32(sqlreader["ModeleId"]);
                    voiture.Couleur = Convert.ToString(sqlreader["Couleur"]);
                    sqlreader.Close();
                    return voiture;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Suprimme une netrée de la table voiture en fonction de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SupprimerVoiture(int id)
        {
            NpgsqlCommand sqlCmd = null;
            NpgsqlTransaction sqltr = null;
            if (CheckVoiture(id) != null)
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM SupprimerVoiture(:id)", this.SqlConn);

                // Ajoute les paramètres
                sqlCmd.Parameters.Add(new NpgsqlParameter("id",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                // Prepare la commande
                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres
                sqlCmd.Parameters[0].Value = id;

                //TRANSACTION
                sqltr = this.SqlConn.BeginTransaction();
                sqlCmd.Transaction = sqltr;

                //int nbrColonneAffectee = sqlCmd.ExecuteNonQuery();
                int nbrColonneAffectee = (int)sqlCmd.ExecuteScalar();

                // Termine la transaction
                sqltr.Commit();

                return nbrColonneAffectee;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Vérifie si une voiture est enregistrée dans la table "VOITURE" ou non en fonction de son id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Voiture CheckVoiture(int id)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Voiture voiture; 
                sqlCmd = new NpgsqlCommand("SELECT * FROM CheckVoiture(:id)", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("id",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = id;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    voiture = new Voiture();

                    voiture.Id = sqlreader["Id"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["Id"]);
                    voiture.Immatriculation = Convert.ToString(sqlreader["Immatriculation"]);
                    voiture.ModeleId = sqlreader["ModeleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["ModeleId"]);
                    voiture.Couleur = Convert.ToString(sqlreader["Couleur"]);
                    sqlreader.Close();
                    return voiture;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }
        /// <summary>
        /// Retourne une entrée de la table EMPLACEMENT en fonction d'un id voiture.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetVoitureLouee(int id)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Voiture voitureLouee = new Voiture();
                int clientId = 0;
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetVoitureLouee(:id)", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("id",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = id;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    voitureLouee.Id = sqlreader["Id"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["Id"]);
                    voitureLouee.Immatriculation = Convert.ToString(sqlreader["Immatriculation"]);
                    voitureLouee.ModeleId = sqlreader["ModeleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["ModeleId"]);
                    voitureLouee.Couleur = Convert.ToString(sqlreader["Couleur"]);
                    clientId = sqlreader["ClientId"] == DBNull.Value ? -1 : Convert.ToInt32(sqlreader["ClientId"]);
                    sqlreader.Close();
                    //return voitureLouee;
                    return clientId;
                }
                else
                {
                    sqlreader.Close();
                    return -1;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }
        #endregion

        #region EMPLACEMENT

        /// <summary>
        /// Liste les emplacements déjà occupés
        /// </summary>
        /// <returns></returns>
        public List<Emplacement> ListEmplacementPris()
        {
            List<Emplacement> liste = null;
            NpgsqlCommand sqlCmd = null;

            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM ListEmplacementPris();", this.SqlConn);

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    liste = new List<Emplacement>();

                    do
                    {
                        liste.Add(new Emplacement(Convert.ToString(sqlreader["Intitule"]),
                                                 Convert.ToInt32(sqlreader["VoitureId"])));
                    } while (sqlreader.Read());

                }
                sqlreader.Close();
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
            return liste;
        }

        /// <summary>
        /// Retourne une entrée de la table EMPLACEMENT en fonction de son intitule.
        /// </summary>
        /// <param name="intitule"></param>
        /// <returns></returns>
        public Emplacement GetEmplacement(string intitule)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Emplacement emplacement = new Emplacement();
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetEmplacement(:intitule);", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("intitule",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = intitule;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    emplacement.Intitule = Convert.ToString(sqlreader["Intitule"]);
                    emplacement.VoitureId = sqlreader["VoitureId"] == DBNull.Value ? (int?)null : Convert.ToInt32(sqlreader["VoitureId"]);
                    sqlreader.Close();
                    return emplacement;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Retourne une entrée de la table EMPLACEMENT en fonction d'un identifiant de voiture.
        /// </summary>
        /// <param name="intitule"></param>
        /// <returns></returns>
        public Emplacement GetEmplacement(int voitureId)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                Emplacement emplacement = new Emplacement();
                sqlCmd = new NpgsqlCommand("SELECT * FROM GetEmplacementById(:voitureId);", this.SqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("voitureId",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Prepare();
                sqlCmd.Parameters[0].Value = voitureId;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    emplacement.Intitule = Convert.ToString(sqlreader["Intitule"]);
                    emplacement.VoitureId = Convert.ToInt32(sqlreader["VoitureId"]);
                    sqlreader.Close();
                    return emplacement;
                }
                else
                {
                    sqlreader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Ajoute une entrée dans la Table EMPLACEMENT
        /// </summary>
        /// <param name="emplacement"></param>
        /// <returns></returns>
        public int AjouterEmplacement(Emplacement emplacement)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM AjouterEmplacement(:intitule, :voitureId)", this.SqlConn);

                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("intitule",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                sqlCmd.Parameters.Add(new NpgsqlParameter("voitureId",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = emplacement.Intitule;
                sqlCmd.Parameters[1].Value = emplacement.VoitureId;

                // Execute la commande

                return ((int)sqlCmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Suprimme une entrée de la table EMPLACEMENT en fonction d'un identifiant d'une voiture.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SupprimerEmplacement(int voitureId)
        {
            NpgsqlCommand sqlCmd = null;
            NpgsqlTransaction sqltr = null;
            if (GetEmplacement(voitureId) != null)
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM SupprimerEmplacement(:voitureId)", this.SqlConn);

                // Ajoute les paramètres
                sqlCmd.Parameters.Add(new NpgsqlParameter("voitureId",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                // Prepare la commande
                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres
                sqlCmd.Parameters[0].Value = voitureId;

                //TRANSACTION
                sqltr = this.SqlConn.BeginTransaction();
                sqlCmd.Transaction = sqltr;

                int nbrColonneAffectee = (int)sqlCmd.ExecuteScalar();

                // Termine la transaction
                sqltr.Commit();

                return nbrColonneAffectee.ToString();
            }
            else
            {
                return "Voiture inexistante en BD.";
            }
        }


        #endregion

        #region LOCATION

        /// <summary>
        /// // Ajouter une voiture
        /// </summary>
        /// <param name="categorie"></param>
        /// <returns></returns>
        public decimal AjouterLocation(Voiture voiture, Client client, DateTime dateRetour)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("Select * FROM AjouterLocation(:voitureId, :clientId, :dateLocation , :dateRetour, '0', :montant)", this.SqlConn);
                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("voitureId",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Parameters.Add(new NpgsqlParameter("clientId",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                sqlCmd.Parameters.Add(new NpgsqlParameter("dateLocation",
                    NpgsqlTypes.NpgsqlDbType.Date));
                sqlCmd.Parameters.Add(new NpgsqlParameter("dateRetour",
                    NpgsqlTypes.NpgsqlDbType.Date));
                //sqlCmd.Parameters.Add(new NpgsqlParameter("estRendue",
                //    NpgsqlTypes.NpgsqlDbType.Bit));
                sqlCmd.Parameters.Add(new NpgsqlParameter("montant",
                    NpgsqlTypes.NpgsqlDbType.Numeric));

                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = voiture.Id;
                sqlCmd.Parameters[1].Value = client.Id;
                sqlCmd.Parameters[2].Value = DateTime.Now;
                sqlCmd.Parameters[3].Value = dateRetour;
                sqlCmd.Parameters[4].Value = CalculMontant(dateRetour, voiture);
                //sqlCmd.Parameters[4].Value = Convert.ToByte(0);
                //sqlCmd.Parameters[5].Value = CalculMontant(dateRetour, voiture);

                // Execute la commande

                //return ((int)sqlCmd.ExecuteScalar());
                int resulat = (int)sqlCmd.ExecuteScalar();
                if (resulat == 1)
                    return CalculMontant(dateRetour, voiture);
                else
                    return -1;
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Calcule le montant d'une location.
        /// </summary>
        /// <param name="dateRetour"></param>
        /// <param name="voiture"></param>
        /// <returns></returns>
        private decimal CalculMontant(DateTime dateRetour, Voiture voiture)
        {
            decimal prixJour = 0;
            TimeSpan jours = (dateRetour - DateTime.Now);
            int nbrJours = (int)jours.TotalDays;


            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM CalculMontant(:voitureId)", this.SqlConn);

                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("voitureId",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = voiture.Id;

                NpgsqlDataReader sqlreader = sqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    prixJour = Convert.ToDecimal(sqlreader["PrixJour"]);
                }
                sqlreader.Close();

                decimal montant = nbrJours * prixJour;
                return montant;

            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }

        /// <summary>
        /// Met à jour une entrée dans la table Location en fonction de son identifiant voiture.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public int UpdateLocation(int voitureId)
        {
            NpgsqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new NpgsqlCommand("SELECT * FROM UpdateLocation(:voitureId)", this.SqlConn);

                // Ajoute les paramètres

                sqlCmd.Parameters.Add(new NpgsqlParameter("voitureId",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                // Prepare la commande

                sqlCmd.Prepare();

                // Ajouter les valeurs aux paramètres

                sqlCmd.Parameters[0].Value = voitureId;

                // Execute la commande

                return ((int)sqlCmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD(sqlCmd.CommandText, e.Message);
            }
        }
        #endregion
    }
}
