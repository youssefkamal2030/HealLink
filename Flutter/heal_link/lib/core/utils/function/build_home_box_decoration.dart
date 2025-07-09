import 'package:flutter/material.dart';

BoxDecoration buildHomeBoxDecoration() {
  return BoxDecoration(
    borderRadius: const BorderRadius.only(
      bottomLeft: Radius.circular(25),
      bottomRight: Radius.circular(25),
    ),
    gradient: LinearGradient(
      colors: [Color(0xffDBEBFF), Color(0xffFFFFFF)],
      begin: AlignmentDirectional.centerEnd,
      end: AlignmentDirectional.centerStart,
    ),
    boxShadow: [
      BoxShadow(
        color: const Color(0x14000000),
        offset: const Offset(-1, 1),
        blurRadius: 4,
      ),
    ],
  );
}
