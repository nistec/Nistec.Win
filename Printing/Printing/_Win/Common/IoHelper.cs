using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using MControl.Win;

namespace MControl
{
	public static class WinIoHelper
	{
		
		#region Methods

		public static void GetRecursiveFiles(string path, string searchPattern, ref ArrayList listFiles)
		{
			string[] filesMain = Directory.GetFiles(path, searchPattern);
			foreach (string file in filesMain)
			{
				if (listFiles.IndexOf(file)==-1)
					listFiles.Add(file);
			}

			foreach(string localPath in Directory.GetDirectories(path))
			{
				listFiles.AddRange(Directory.GetFiles(localPath, searchPattern));
				GetRecursiveFiles(localPath, searchPattern, ref listFiles);
			}
		}

		public static bool ReplaceFile(string tempFile, string targetFile, bool deleteTempFile)
		{
			bool bStatus = false;

			if (CheckWriteAccess(targetFile))
			{
				File.Copy(tempFile, targetFile, true);
				bStatus = true;
			}
	
			if (deleteTempFile) File.Delete(tempFile);
			return bStatus;
		}

		public static bool CheckWriteAccess(string fileName)
		{
			int iCount = 0; 
			const int iLimit = 10; 
			const int iDelay = 200;

			while (iCount < iLimit)
			{
				try
				{
					FileStream fs;
					fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None);
					fs.Close();
					return true;
				}
				catch
				{
					Thread.Sleep(iDelay);
				}
				finally
				{
					iCount += 1;
				}
			}
			return false;
		}

		[System.ComponentModel.Description("Read text from file")]
		public static string ReadTextFile(string filePath)
		{
			try
			{
				// Read the file as one string.
				System.IO.StreamReader _File =
					new System.IO.StreamReader(filePath);
				string _String = _File.ReadToEnd();

				_File.Close();
				return _String;
			}
			catch(Exception ex)
			{
               throw ex;
			}
		}

		[System.ComponentModel.Description("Open text file and Read text from it")]
		public static string ReadTextFile()
		{
            string fileName = CommonDlg.FileDialog("");   
      		try
			{
				if(fileName.Length >0)
				{
					// Read the file as one string.
					System.IO.StreamReader _File =
						new System.IO.StreamReader(fileName);
					string _String = _File.ReadToEnd();

					_File.Close();
					return _String;
				}
				return "";
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region "public static Xml Serializations"
        
		/// <summary>
		/// Get object from an xml string.
		/// </summary>
		/// <param name="xmlString"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static object XmlStringToObject(string xmlString, System.Type type)
		{
			System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(type);
			TextReader r = null;
			object retObj = null;
			try 
			{
				r = new StringReader( xmlString );
				retObj = x.Deserialize(r);
			}
			finally
			{
				if (r != null)
					r.Close();
			}
			return retObj;
		}

		/// <summary>
		/// Write object to xml string.
		/// </summary>
		/// <param name="obj"></param>
		public static string ObjectToXmlString(object obj)
		{
			System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(obj.GetType());
			TextWriter w = null;
			string sReturn = string.Empty;

			try 
			{
				w = new StringWriter(); 
				x.Serialize(w, obj);
				sReturn = w.ToString();
			} 
			finally
			{
				if (w != null)
					w.Close();
			}

			return sReturn;
		}
         
		#endregion

		#region "public static FileToString"

		/// <summary>
		/// Load the entire text file into a string.
		/// </summary>
		/// <param name="sFile">Full pathname of file to read.</param>
		/// <returns>String content of the text file.</returns>
		public static string FileToString(string sFile)
		{
			string sText = string.Empty;
			using (StreamReader sr = new StreamReader(sFile))
			{
				sText = sr.ReadToEnd();
			}
			return sText;
		}

		/// <summary>
		/// Load the text file with specified size as return text.
		/// </summary>
		/// <param name="sFile">File to read from.</param>
		/// <param name="size">Number of char to read.</param>
		/// <returns></returns>
		public static string FileToString(string sFile, int size)
		{
			char[]  cToRead = new char[size];
			string sText = string.Empty;
			using(StreamReader sr = new StreamReader(sFile))
			{
				sr.Read(cToRead, 0, size);
				sText = new string(cToRead);
			}
			return sText;
		}
		#endregion

		#region "public static StringToFile"

		/// <summary>
		/// Save a string to file.
		/// </summary>
		/// <param name="strValue">String value to save.</param>
		/// <param name="strFileName">File name to save to.</param>
		/// <param name="bAppendToFile">True - to append string to file.  Default false - overwrite file.</param>
		public static void StringToFile(string strValue, string strFileName, bool bAppendToFile)
		{
			using(StreamWriter sw = new StreamWriter(strFileName, bAppendToFile))
			{
				sw.Write(strValue);
			}
		}

		/// <summary>
		/// Save a string to file.
		/// </summary>
		public static void StringToFile(string strValue, string strFileName)
		{
			StringToFile(strValue, strFileName, false);
		}
		#endregion

        #region stream

        public static string ReadFileStream(string fileName)
        {
            String input = null;
            if (!File.Exists(fileName))
            {
                Console.WriteLine("{0} does not exist!", fileName);
                return null;
            }
            FileStream fsIn = new FileStream(fileName, FileMode.Open,
                FileAccess.Read, FileShare.Read);
            // Create an instance of StreamReader that can read 
            // characters from the FileStream.
            StreamReader sr = new StreamReader(fsIn);
            // While not at the end of the file, read lines from the file.
            input = sr.ReadToEnd();

            Console.WriteLine(input);
            sr.Close();

            return input;
        }

        public static byte[] ReadBinaryStream(string fileName)
        {
            byte[] output = null;

            if (!File.Exists(fileName))
            {
                Console.WriteLine("{0} does not exist.", fileName);
                return null;
            }
            FileStream f = new FileStream(fileName, FileMode.Open,
                FileAccess.Read, FileShare.Read);
            // Create an instance of BinaryReader that can 
            // read bytes from the FileStream.
            BinaryReader sr = new BinaryReader(f);
            // While not at the end of the file, read lines from the file.

            output = sr.ReadBytes((int)f.Length);
            Console.WriteLine(output);
           
            sr.Close();
            return output;
        }

        public static byte[] ImageToStream(string fileName)
        {
            Console.WriteLine("Processing images... ");
            //const int size = 4096;
            long t0 = Environment.TickCount;
            byte[] pixels = null;
            FileStream input = null ;
            try
            {
                input = new FileStream(fileName,
                    FileMode.Open, FileAccess.Read, FileShare.Read);//,size,false);
                int len = (int)input.Length;
                pixels = new byte[len];
                input.Read(pixels, 0, len);
                long t1 = Environment.TickCount;
                Console.WriteLine("Total time processing images: {0}ms", (t1 - t0));
                return pixels;
            }
            finally
            {
                input.Close();
                input = null;
            }
        }

        public static string ImageToBase64Stream(string fileName)
        {
            try
            {
                return Convert.ToBase64String(ImageToStream(fileName));
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                return null;
            }
        }

        public static string ImageToBase64Stream(System.Drawing.Image image,System.Drawing.Imaging.ImageFormat format)
        {

            byte[] bytes = null;

            try
            {
                using (System.IO.Stream stream = new System.IO.MemoryStream())
                {
                    image.Save(stream, format);
                    bytes = new byte[stream.Length];
                    stream.Read(bytes, 0,(int) stream.Length);

                    stream.Close();
                }

                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return null;
            }
        }
        
        public static System.Drawing.Image ImageFromBase64Stream(string s)
        {
            System.Drawing.Image image = null;
            byte[] bytes = System.Convert.FromBase64String(s);
            using (System.IO.Stream stream = new System.IO.MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(stream);
                stream.Close();
            }
            return image;
        }

        public static void ProcessImage(string fileName, string base64Stream)
        {
            Console.WriteLine("Processing images... ");
            long t0 = Environment.TickCount;
            byte[] pixels = null;
            FileStream output=null;
            try
            {
                pixels = Convert.FromBase64String(base64Stream);
                output = new FileStream(fileName,
                   FileMode.Create, FileAccess.Write, FileShare.None,
                   pixels.Length, false);
                output.Write(pixels, 0, pixels.Length);
                long t1 = Environment.TickCount;
                Console.WriteLine("Total time processing images: {0}ms", (t1 - t0));
            }
            finally
            {
                output.Close();
                output = null;
            }
        }

        public static string GetResourceStream(System.Reflection.Assembly assembly, string fileName)
        {
            string text = null;
            Stream stream1 = assembly.GetManifestResourceStream(fileName);
            if (stream1 != null)
            {
                stream1.Position = 0;
                using (StreamReader reader1 = new StreamReader(stream1))
                {
                    text = reader1.ReadToEnd();
                }
            }
            return text;
        }

        #endregion

        #region memory stream read
        /// <summary>
        /// Converts a string to a MemoryStream.
        /// </summary>
       /// <param name="s"></param>
       /// <returns></returns>
        public static MemoryStream StringToMemoryStream(string s)
        {
            byte[] a = System.Text.Encoding.Default.GetBytes(s);
            return new System.IO.MemoryStream(a);
        }


        /// <summary>
        /// Converts a string to a MemoryStream.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static MemoryStream StringToMemoryStream(string s,string encoding)
        {
            byte[] a = System.Text.Encoding.GetEncoding(encoding).GetBytes(s);
            return new System.IO.MemoryStream(a);
        }

        /// <summary>
        /// Converts a MemoryStream to a string. Makes some assumptions about the content of the stream. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String MemoryStreamToString(MemoryStream ms)
        {
            byte[] ByteArray = ms.ToArray();
            return System.Text.Encoding.Default.GetString(ByteArray);
        }
        /// <summary>
        /// Converts a MemoryStream to a string. Makes some assumptions about the content of the stream. 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String MemoryStreamToString(MemoryStream ms,string encoding)
        {
            byte[] ByteArray = ms.ToArray();
            return System.Text.Encoding.GetEncoding(encoding).GetString(ByteArray);
        }

        /// <summary>
        /// Copy Stream
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="length"></param>
        public static void CopyStream(System.IO.Stream src, System.IO.Stream dest, int length)
        {
            byte[] buffer = new byte[length];
            int len = src.Read(buffer, 0, buffer.Length);
            while (len > 0)
            {
                dest.Write(buffer, 0, len);
                len = src.Read(buffer, 0, buffer.Length);
            }
            dest.Flush();
        }

  
        /// <summary>
        /// Reads data into a complete array, throwing an EndOfStreamException
        /// if the stream runs out of data first, or if an IOException
        /// naturally occurs.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="data">The array to read bytes into. The array
        /// will be completely filled from the stream, so an appropriate
        /// size must be given.</param>
        public static void ReadSream(Stream stream, byte[] data)
        {
            int offset = 0;
            int remaining = data.Length;
            while (remaining > 0)
            {
                int read = stream.Read(data, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException
                        (String.Format("End of stream reached with {0} bytes left to read", remaining));
                remaining -= read;
                offset += read;
            }
        }


        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        public static byte[] ReadSream(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }


        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        public static byte[] ReadSream(Stream stream, int initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }
        #endregion

        #region files

        /// <summary>
        /// Returns the names of files in a specified directories that match the specified patterns using LINQ
        /// </summary>
        /// <param name="srcDirs">The directories to seach</param>
        /// <param name="searchPatterns">the list of search patterns</param>
        /// <param name="searchOption"></param>
        /// <returns>The list of files that match the specified pattern</returns>
        public static string[] GetFiles(string[] srcDirs,
             string[] searchPatterns,
             SearchOption searchOption = SearchOption.AllDirectories)
        {
            var r = from dir in srcDirs
                    from searchPattern in searchPatterns
                    from f in Directory.GetFiles(dir, searchPattern, searchOption)
                    select f;

            return r.ToArray();
        }

       #endregion

        #region Process

        public static string RunProcessWithResults(string url, string args)
        {
            string response = null;

            ProcessStartInfo psi = new ProcessStartInfo(url);
            psi.RedirectStandardOutput = true;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.UseShellExecute = false;
            psi.Arguments = args;
            //Process proc;
            using (Process exeProcess = Process.Start(psi))
            {
                System.IO.StreamReader stream = exeProcess.StandardOutput;
                exeProcess.WaitForExit();// (2000);
                if (exeProcess.HasExited)
                {
                    string output = stream.ReadToEnd();
                    response = output;
                }
            }
            return response;
        }

        public static void RunProcess(string url, string args)
        {

            // For the example
            //string alarmFileName = SrvConfig.SystemAlarmUrl;
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = url;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = args;


            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        #endregion

        public static void OpenFile(string file)
        {
            ExecuteProcessFile(file);
        }

        public static void ExecuteProcessFile(string command)
        {
            System.Diagnostics.ProcessStartInfo p = new System.Diagnostics.ProcessStartInfo(command);
            p.UseShellExecute = true;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = p;
            process.Start();
        }

    }
}
