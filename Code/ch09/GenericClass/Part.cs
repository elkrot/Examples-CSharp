using System;

namespace GenericClass
{
    //the object we want to add to our index
    class Part
    {
        private string _partId;
        private string _name;
        private string _description;
        private double _weight;

        public string PartId { get { return _partId; } }

        public Part(string partId, string name, string description, double weight)
        {
            _partId = partId;
            _name = name;
            _description = description;
            _weight = weight;
        }

        public override string ToString()
        {
            return string.Format("Part: {0}, Name: {1}, Weight: {2}", _partId, _name, _weight);
        }
    }
}