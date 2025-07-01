import 'package:flutter/cupertino.dart';

BoxDecoration buildBoxDecoration() {
  return const BoxDecoration(
    gradient: LinearGradient(
      begin: Alignment.topRight,
      end: Alignment.bottomLeft,
      colors: [
        Color(0xFFADC5F5),
        Color(0xFFF4F6F8),
        Color(0xFFFFFFFF),
      ],
      stops: [0.0, 0.6, 1.0],
    ),
  );
}