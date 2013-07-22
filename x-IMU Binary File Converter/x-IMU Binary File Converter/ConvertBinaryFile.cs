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
        private x_IMU_API.CSVfileWriter convertedBinaryFiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConvertBinaryFile"/> class.
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
        public x_IMU_API.PacketCount Convert()
        {
            convertedBinaryFiles = new x_IMU_API.CSVfileWriter(Path.GetDirectoryName(FilePath) + "\\" + Path.GetFileNameWithoutExtension(FilePath));
            x_IMU_API.xIMUfile xIMUfile = new x_IMU_API.xIMUfile(FilePath);
            xIMUfile.xIMUdataRead += new x_IMU_API.xIMUfile.onxIMUdataRead(xIMUfile_xIMUdataRead);
            xIMUfile.Read();
            xIMUfile.Close();
            convertedBinaryFiles.CloseFiles();
            return xIMUfile.PacketCounter;
        }

        /// <summary>
        /// x-IMU data read event to write data to ASCII files.
        /// </summary>
        void xIMUfile_xIMUdataRead(object sender, x_IMU_API.xIMUdata e)
        {
            try
            {
                convertedBinaryFiles.WriteData(e);
            }
            catch { }
        }
    }
}