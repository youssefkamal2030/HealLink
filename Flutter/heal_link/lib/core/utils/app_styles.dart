import 'package:flutter/material.dart';

import 'function/app_colors.dart';

abstract class AppTextStyles {
  static const bold16 = TextStyle(
    color: Color(0xff1C1F23),
    fontFamily: 'Poppins',
    fontWeight: FontWeight.w700,
    fontSize: 16,
  );
  static const normal14 = TextStyle(
    color: Color(0xff1C1F23),
    fontFamily: 'Poppins',
    fontWeight: FontWeight.w400,
    fontSize: 14,
  );
  static TextStyle normal16 = TextStyle(
    color: AppColors.kPrimaryColor,
    fontFamily: 'Roboto',
    fontWeight: FontWeight.w400,
    fontSize: 16,
  );
  static TextStyle regular16 = TextStyle(
    color: AppColors.kPrimaryColor,
    fontFamily: 'Inter',
    fontWeight: FontWeight.w500,
    fontSize: 16,
  );
}
