﻿#version 330 core
layout (location = 0) in vec3 aPosition;

uniform mat4 view;

void main()
{
    gl_Position = view * vec4(aPosition, 1.0);
		gl_PointSize = 5.0;
}