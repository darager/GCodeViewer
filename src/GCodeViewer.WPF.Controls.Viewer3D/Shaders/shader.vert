#version 330 core

layout (location = 0) in vec3 aPosition;
uniform mat4 %UNIFORMNAME%;

void main()
{
   gl_PointSize = 1.0;
   gl_Position = %UNIFORMNAME% * vec4(aPosition, 1.0);
}
