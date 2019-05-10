using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aras.IOM;

namespace ArasCLI
{
    // C# program to illustrate the 
    // Initialization of an object 
    using System;

    // Class Declaration 
    public class fileListItem
    {
        public string name { get; set; }
        public string extension { get; set; }
        public string filesize { get; set; }
        public string fullname { get; set; }
        public string directory { get; set; }
        public bool uploaded { get; set; }
        public string arasId { get; set; }
        public fileListItem(string name, string extension, string filesize, string fullname, string directory)
        {
            this.name = name;
            this.extension = extension;
            this.filesize = filesize;
            this.fullname = fullname;
            this.directory = directory;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            
            string login = "";
            string password = "";
            string database = "";
            string url = "";
            string configFile = "";
            string filepathin = "";
            string filepathout = "";
            string filepathlog = "";
            string mode = "aml";
            string folderPath = "";
            bool recursiveSearch = false;

            System.Console.WriteLine(@"


                               _____                             _                _____ _      _____ 
        /\                   |_   _|                           | |              / ____| |    |_   _|
       /  \   _ __ __ _ ___    | |  _ __  _ __   _____   ____ _| |_ ___  _ __  | |    | |      | |  
      / /\ \ | '__/ _` / __|   | | | '_ \| '_ \ / _ \ \ / / _` | __/ _ \| '__| | |    | |      | |  
     / ____ \| | | (_| \__ \  _| |_| | | | | | | (_) \ V / (_| | || (_) | |    | |____| |____ _| |_ 
    /_/    \_\_|  \__,_|___/ |_____|_| |_|_| |_|\___/ \_/ \__,_|\__\___/|_|     \_____|______|_____|
                                                                                                 
                                                                                                 
    V0.1

");

            // Test if any input arguments were supplied:
            if (args.Length == 0)
            {
                // display available commands
                System.Console.WriteLine(@"
    AML Loader

    =========== SHORT ===========

    Aras Innovator CLI is an open source project for communicating 
    with Aras Innovator PLM Solution through a command line

    -h or --help for more information

    =========== MANDATORY ===========
    -l  <url>           => Aras URL
    -d  <dbname>        => Aras Database
    -u  <user login>    => Aras User
    -p  <password>      => Aras Password
    -f  <filepath>      => Input AML File
                ");
                return;
            }

            // test if first argument is -h
            if ((args[0] == "-h") || (args[0] == "--help"))
            {

                System.Console.WriteLine(@"


    =========== DESCRIPTION ===========


    =========== USAGE SYNTAX ===========


    =========== OPTIONS ===========


    ==> MANDATORY 
    -l  <url>           => Aras URL
    -d  <dbname>        => Aras Database
    -u  <user login>    => Aras User
    -p  <password>      => Aras Password
    -f  <filepath>      => Input AML File

    ==> OPTIONNAL
    -m  <mode>          => CLI mode ('aml' load, 'files' upload) default is aml
    -g  <filepath>      => Log output file
    -o  <filepath>      => Result output file
    -c  <filepath>      => Instance Config File 
    -fop  <filepath>    => folder path for file upload
    -rec  <boolean>     => if set then file upload browse target folder recursively
                       template : 
                            l:http://localhost/InnovatorServer
                            d:InnovatorSolution
                            u:admin
                            p:innovator                             

                ");
                return;
            }

            System.Console.WriteLine("");
            // read input arguments
            for (int i = 0; i < args.Length; i++)
            {

                switch (args[i])
                {
                    case "-m":
                    case "--mode":
                        mode = args[i + 1];
                        System.Console.WriteLine(" - Mode = " + args[i + 1]);
                        break;
                    case "-fop":
                    case "--folderPath":
                        folderPath = args[i + 1];
                        System.Console.WriteLine(" - folderPath = " + args[i + 1]);
                        break;
                    case "-rec":
                    case "--recursiveSearch":
                        recursiveSearch = true;
                        System.Console.WriteLine(" - recursiveSearch = true" );
                        break;
                    case "-c":
                    case "--config":
                        configFile = args[i + 1];
                        System.Console.WriteLine(" - Config Input = " + args[i + 1]);
                        break;
                    case "-l":
                    case "--url":
                        url = args[i + 1];
                        System.Console.WriteLine(" - URL = " + args[i + 1]);
                        break;
                    case "-d":
                    case "--database":
                        database = args[i + 1];
                        System.Console.WriteLine(" - Db = " + args[i + 1]);
                        break;
                    case "-u":
                    case "--user":
                        login = args[i + 1];
                        System.Console.WriteLine(" - Login = " + args[i + 1]);
                        break;
                    case "-p":
                    case "--password":
                        password = args[i + 1];
                        System.Console.WriteLine(" - Password = *****");
                        break;
                    case "-f":
                    case "--inputfile":
                        filepathin = args[i + 1];
                        System.Console.WriteLine(" - Source file = " + args[i + 1]);
                        break;
                    case "-o":
                    case "--outputfile":
                        filepathout = args[i + 1];
                        System.Console.WriteLine(" - Output file = " + args[i + 1]);
                        break;
                    case "-g":
                    case "--log":
                        filepathlog = args[i + 1];
                        System.Console.WriteLine(" - Output file = " + args[i + 1]);
                        break;

                }
            }

            System.Console.WriteLine("");
            // test if config file is available and read the content
            System.Console.WriteLine(configFile);

            if (configFile != "")
            {
                string[] configContent;
                try
                {
                    configContent = File.ReadAllLines(configFile);

                    foreach (string line in configContent)
                    {
                        switch (line.Substring(0, 2))
                        {
                            case "l:":
                                url = line.Substring(2);
                                break;
                            case "d:":
                                database = line.Substring(2);
                                break;
                            case "u:":
                                login = line.Substring(2);
                                break;
                            case "p:":
                                password = line.Substring(2);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return;
                }
            }


            // test if mandatory arguments are provided
            if (login != "" && password != "" && database != "" && url != "")
            {
                // Aras Connection
                System.Console.WriteLine("... connection ...");
                HttpServerConnection conn;
                Innovator inn;
                try
                {
                    conn = IomFactory.CreateHttpServerConnection(url, database, login, password);
                    Item login_result = conn.Login();
                    System.Console.WriteLine("Log in result : " + !login_result.isError());
                    if (login_result.isError())
                    {
                        System.Console.WriteLine(login_result.getErrorString());
                        return;
                    }
                    System.Console.WriteLine("Instanciate Innovator");
                    inn = IomFactory.CreateInnovator(conn);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return;
                }

                switch(mode)
                {
                    case "aml":
                        System.Console.WriteLine("");
                        // read AML
                        System.Console.WriteLine("Read AML file");
                        string readAML;
                        try
                        {
                            readAML = File.ReadAllText(filepathin);
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex);
                            return;
                        }

                        System.Console.WriteLine("Commit AML file");
                        Item result = inn.applyAML(readAML);
                        
                        if (result.isError())
                        {
                            System.Console.Write(result.getErrorString());
                        }
                        else
                        {
                            System.Console.Write(result.dom.OuterXml);

                        }
                        
                        if (filepathout != "")
                        {
                            try
                            {
                                File.WriteAllText(filepathout, result.dom.OuterXml);
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine(ex);
                                return;
                            }
                        }

                        if (filepathlog != "")
                        {
                            try
                            {
                                File.WriteAllText(filepathlog, result.dom.OuterXml);
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine(ex);
                                return;
                            }
                        }
                        break;

                    case "files":

                        System.Console.WriteLine("start import");

                        List<fileListItem> fullFileList  = new List<fileListItem>();

                        DirectoryInfo direct = new DirectoryInfo(folderPath);
                        foreach (var File in direct.GetFiles())
                        {
                            fileListItem fileItem = new fileListItem(File.Name, File.Extension, convertFileSize(File.Length), File.FullName, File.Directory.Name);
                            fullFileList.Add(fileItem);
                        }
                        if (recursiveSearch)
                        {
                            DirSearch(folderPath, recursiveSearch, fullFileList);
                        }

                        // create file
                        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                        string csvfilename = "fileImport_" + unixTimestamp.ToString() + ".csv";
                        StringBuilder csvContent = new StringBuilder();

                        System.Console.WriteLine("import csv file result : " + csvfilename);

                        foreach (fileListItem fli in fullFileList)
                           
                          
                        {
                            System.Console.WriteLine("import " + fullFileList.Count + " files");
                            Item newFile = inn.newItem("File", "add");
                            newFile.setProperty("filename", fli.name);
                            newFile.attachPhysicalFile(fli.fullname);

                            System.Console.WriteLine(newFile.dom.OuterXml.ToString());
                            Item resultUpload = newFile.apply();
                            System.Console.WriteLine(resultUpload.dom.OuterXml.ToString());
                            if (resultUpload.isError())
                            {
                                System.Console.WriteLine("Error when importing");
                                return;
                            }
                            else
                            {
                                csvContent.Append(fli.directory);
                                fli.uploaded = true;
                                csvContent.Append(";" + fli.name);
                                fli.arasId = resultUpload.getID();
                                csvContent.Append(";" + resultUpload.getID());
                                csvContent.AppendLine();
                            }
                        }

                       
                        string filepath= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + csvfilename;
                        File.WriteAllText(filepath, csvContent.ToString());
                        System.Console.WriteLine(@" output csv file saved at : "+ filepath);

                        break;
                }
               

                conn.Logout();
                System.Console.WriteLine(@"

                ");
            }
            else
            {

                System.Console.WriteLine(@"
 No connection settings provided !
                ");
            }
        }


        
        static String convertFileSize(long fileLength)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (fileLength >= 1024 && order + 1 < sizes.Length)
            {
                order = order + 1;
                fileLength = fileLength / 1024;
            }
            string result = String.Format("{0:0.##} {1}", fileLength, sizes[order]);
            return result;
        }

        public static List<fileListItem> DirSearch(string sDir, Boolean recursive, List<fileListItem> actualList)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    DirectoryInfo direct = new DirectoryInfo(d);
                    foreach (var File in direct.GetFiles())
                    {
                        fileListItem fileItem = new fileListItem(File.Name, File.Extension, convertFileSize(File.Length), File.FullName, File.Directory.Name);
                        actualList.Add(fileItem);
                        if (recursive)
                            DirSearch(d, recursive, actualList);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return actualList;
        }

    }
}
