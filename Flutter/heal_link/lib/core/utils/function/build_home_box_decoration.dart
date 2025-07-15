import 'package:flutter/material.dart';

BoxDecoration buildHomeBoxDecoration({bool isPatient = false}) {
  return BoxDecoration(
    borderRadius:
        isPatient
            ? BorderRadius.circular(8)
            : const BorderRadius.only(
              bottomLeft: Radius.circular(25),
              bottomRight: Radius.circular(25),
            ),

    gradient: LinearGradient(
      colors: [Color(0xFFDBEBFF), Color(0xFFFFFFFF)],
      begin: AlignmentDirectional.centerEnd,
      end: AlignmentDirectional.centerStart,
      stops: isPatient ? [-0.7126, 0.7791] : null,
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
