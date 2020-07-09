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
            var BaseMesh = CutAndCloseMesh(position, normal);
            var CutOffMesh = CutAndCloseMesh(position, OppositeDirection(normal));

            return (BaseMesh, CutOffMesh);
        }

        private Mesh CutAndCloseMesh(Vector3d position, Vector3d normal)
        {
            // new Mesh has to be created since this method is not functional
            var cut = new MeshPlaneCut(new DMesh3(this), position, normal);
            cut.Cut();
            CloseCuttingSurfaceHoles(cut);

            return new Mesh(cut.Mesh);
        }

        private void CloseCuttingSurfaceHoles(MeshPlaneCut cut)
        {
            foreach (var loop in cut.CutLoops)
            {
                var holeFill = new MinimalHoleFill(cut.Mesh, loop);
                holeFill.Apply();
            }
        }

        private Vector3d OppositeDirection(Vector3d vect) => new Vector3d(-vect.x, -vect.y, -vect.z);
    }
}
