using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Unit.UnitCustomAnimations;

namespace Unit
{
    public class UnitInitializer
    {
        public UnitCustomAnimations _unitCustomAnimations;

        internal void GetDefaultLocals(Vector3 body,Vector3 head,Vector3 apparel)
        {
            _unitCustomAnimations = new UnitCustomAnimations
            {
                BodyDefaultLocal = body,
                HeadDefaultLocal = head,
                ApparelDefaultLocal = apparel
            };
        }
        internal void GetUnit(Transform unit)
        {
            _unitCustomAnimations.Body = unit;
            _unitCustomAnimations.Head = _unitCustomAnimations.Body.Find("Head");
            _unitCustomAnimations.Hair = _unitCustomAnimations.Head.Find("Hair");
            _unitCustomAnimations.Apparel = _unitCustomAnimations.Body.Find("Apparel");
            _unitCustomAnimations.Body.localPosition = _unitCustomAnimations.BodyDefaultLocal;
            _unitCustomAnimations.Head.localPosition = _unitCustomAnimations.HeadDefaultLocal;
            _unitCustomAnimations.Apparel.localPosition = _unitCustomAnimations.ApparelDefaultLocal;
            _unitCustomAnimations.HitLight = _unitCustomAnimations.Body.GetComponentInChildren<Light2D>();
            GetAllMaterials();
        }

        private void GetAllMaterials()
        {
            _unitCustomAnimations.AllMaterials=new []
            {
                _unitCustomAnimations.Body.GetComponent<Renderer>().material,
                _unitCustomAnimations.Head.GetComponent<Renderer>().material,
                _unitCustomAnimations.Hair.GetComponent<Renderer>().material,
                _unitCustomAnimations.Apparel.GetComponent<Renderer>().material
            };
        }
    }
}