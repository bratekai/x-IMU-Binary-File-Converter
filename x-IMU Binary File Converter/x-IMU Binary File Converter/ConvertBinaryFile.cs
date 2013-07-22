using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace x_IMU_Binary_File_Converter
{
    /// <summary>
    /// Convert binary file class.
    /// </summary>
    class ConvertBinaryFile
    {
        /// <summary>
        /// Path of binary file.
        /// </summary>
        private string FilePath;

        /// <summary>
        /// ASCII files object for converted binary files.
        /// </summary>
        private xIMU_API.ASCIIdataFiles convertedBinaryFiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConvertBinary"/> class.
        /// </summary>
        /// <param name="filePath">
        /// Path of binary file.
        /// </param>
        public ConvertBinaryFile(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Converts binary file to separate CSV files.
        /// </summary>
        /// <returns>
        /// PacketCounter result.
        /// </returns>
        public xIMU_API.PacketCount Convert()
        {
            convertedBinaryFiles = new xIMU_API.ASCIIdataFiles(Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileNameWithoutExtension(FilePath));
            xIMU_API.xIMUfile xIMUfile = new xIMU_API.xIMUfile(FilePath);
            xIMUfile.xIMUdataRead += new xIMU_API.xIMUfile.onxIMUdataRead(xIMUfile_xIMUdataRead);
            xIMUfile.Read();
            xIMUfile.Close();
            convertedBinaryFiles.CloseFiles();
            return xIMUfile.PacketCounter;
        }

        /// <summary>
        /// x-IMU data read event to write data to ASCII files.
        /// </summary>
        void xIMUfile_xIMUdataRead(object sender, xIMU_API.xIMUdata e)
        {
            try
            {
                convertedBinaryFiles.WriteData(e);
            }
            catch { }
        }
    }
}