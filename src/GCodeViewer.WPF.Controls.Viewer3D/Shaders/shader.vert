#version 330 core

layout (location = 0) in vec3 aPosition;
uniform mat4 transform;
uniform vec3 translation;

void main()
{
   gl_PointSize = 1.0;
   gl_Position = transform * vec4(aPosition + translation, 1.0);
}
