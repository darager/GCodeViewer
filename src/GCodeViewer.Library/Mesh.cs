using g3;
using gs;

namespace GCodeViewer.Library
{
    public class Mesh : DMesh3
    {
        public Mesh(DMesh3 mesh) : base(mesh)
        {
        }

        public (Mesh BaseMesh, Mesh CutOffMesh) Cut(Vector3d position, Vector3d normal)
        {
            var treeCut = new MeshPlaneCut(new DMesh3(this), position, normal);
            treeCut.Cut();
            CloseHoleInMesh(treeCut);

            var branchCut = new MeshPlaneCut(new DMesh3(this), position, new Vector3d(-normal.x, -normal.y, -normal.z));
            branchCut.Cut();
            CloseHoleInMesh(branchCut);

            return (new Mesh(treeCut.Mesh), new Mesh(branchCut.Mesh));
        }

        private void CloseHoleInMesh(MeshPlaneCut cut)
        {
            foreach (var loop in cut.CutLoops)
            {
                var holeFill = new MinimalHoleFill(cut.Mesh, loop);
                holeFill.Apply();
            }
        }
    }
}
