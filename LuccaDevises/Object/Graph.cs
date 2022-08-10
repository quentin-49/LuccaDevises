using LuccaDevises.Exception;
using LuccaDevises.Ressource;
using System.Globalization;

namespace LuccaDevises.Object
{
    public sealed class Graph
    {
        public List<CurrencyChange> DeviceChanges { get; set; }
        public List<Currency> Devices { get; set; }
        public Request Request { get; set; }
        public List<Record> RecordList { get; set; }
        public int DeviceNumber { get; set; }

        private static Graph? _instance;

        public static Graph GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Graph();
            }
            return _instance;
        }

        public Graph()
        {
            DeviceChanges = new List<CurrencyChange>();
            Devices = new List<Currency>();
            RecordList = new List<Record>();
            Request = new Request();
        }

        public void AddDeviceChange(string data)
        {
            string[] datas = data.Split(";");

            decimal.TryParse(datas[2], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal dec);

            AddDeviceChange(datas[0], datas[1], dec);
        }

        private void AddDeviceChange(string startPoint, string endPoint, decimal weight)
        {
            DeviceChanges.AddDeviceChange(startPoint, endPoint, weight);
        }

        /// <summary>
        /// Gènene la liste des devices présentes dans la liste
        /// </summary>
        /// <exception cref="IntegrationDataException"></exception>
        public void GenerateListOfDevice()
        {
            if (DeviceNumber != DeviceChanges.Count())
                throw new IntegrationDataException(Constant.IncoherentDeviceChanges);

            AddDevices(DeviceChanges);
        }

        /// <summary>
        /// Génère les devices qui servirait de liste d'état.
        /// </summary>
        /// <returns></returns>
        public void AddDevices(List<CurrencyChange> deviceChangeList)
        {
            foreach (var point in deviceChangeList.Select(l => l.StartPoint).Union(deviceChangeList.Select(l => l.EndPoint)).Distinct().ToList())
            {
                Devices.Add(new Currency(point, null));
            }
        }

        public Currency GetStartedDevice()
        {
            Currency p = Devices.First(p => p.Name == Request.StartDevice);
            p.Weight = 1;
            p.Marked = true;
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        public Currency? DeviceProcessing(Currency device)
        {
            List<CurrencyChange> deviceChangeList = DeviceChanges.GetDeviceChangeFromName(device.Name);

            CalculDevice(device, deviceChangeList);

            device.Processed = true;

            RegisterRecord(device);

            return GetMinDevice();
        }

        /// <summary>
        /// Met à jour les devices approximité de celle passé en paramêtre
        /// </summary>
        /// <param name="device"></param>
        /// <param name="deviceChangeList"></param>
        private void CalculDevice(Currency device, List<CurrencyChange> deviceChangeList)
        {
            foreach (var seg in deviceChangeList)
            {
                var deviceProcessing = Devices.FirstOrDefault(p => p.Name == seg.EndPoint && !p.Processed);

                if (deviceProcessing == null)
                    continue;

                if (deviceProcessing.Weight == null || deviceProcessing.Weight > device.Weight * seg.Weight)
                {
                    deviceProcessing.Weight = Math.Round((device.Weight ?? 1) * seg.Weight, 4);
                    deviceProcessing.Predecessor = seg.StartPoint;
                }

                deviceProcessing.Marked = true;
            }
        }

        /// <summary>
        /// Retourne le résultat du programme en cherchant le point enregistré.
        /// </summary>
        /// <returns></returns>
        public decimal GetResult()
        {
            if (!RecordList.Any(r => r.StartPoint == Request.EndDevice))
                throw new ResultException(Constant.NoResultCalculeted);

            return RecordList.Where(r => r.StartPoint == Request.EndDevice).Select(r => Math.Ceiling(r.Weight * Request.Amount)).First();
        }

        /// <summary>
        /// Enregistre le chemin choisi pour la résolution finale
        /// </summary>
        /// <param name="pointRecord"></param>
        private void RegisterRecord(Currency pointRecord)
        {
            RecordList.Add(new Record(pointRecord.Name, pointRecord.Predecessor, pointRecord.Weight ?? 0));
        }

        /// <summary>
        /// Recherche la device calculé la plus faible
        /// Device qui n'a pas encore été traité et qui dans le sous graph (device.Marked = 1)
        /// </summary>
        /// <returns></returns>
        private Currency? GetMinDevice()
        {
            if (!Devices.Any(pt => pt.Weight != 0 && !pt.Processed && pt.Marked))
                return null;

            return Devices
                        .OrderBy(d => d.Weight)
                        .FirstOrDefault(d => d.Weight != 0
                            && d.Weight != null
                            && !d.Processed
                            && d.Marked);
        }
    }
}
