import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

ThemeData buildThemeData() {
  return ThemeData(
    colorScheme: ColorScheme.fromSeed(seedColor: AppColors.kPrimaryColor),
    primarySwatch: Colors.blue,
    primaryColor: AppColors.kPrimaryColor,
    primaryColorLight: AppColors.kPrimaryColor,
    bottomNavigationBarTheme: BottomNavigationBarThemeData(
      backgroundColor: AppColors.kBackgroundColor,
      selectedItemColor: AppColors.kPrimaryColor,
      unselectedItemColor: AppColors.kLightGreyColor,
      type: BottomNavigationBarType.fixed,
    ),
    buttonTheme: ButtonThemeData(buttonColor: AppColors.kPrimaryColor),
    textButtonTheme: TextButtonThemeData(
      style: TextButton.styleFrom(
        foregroundColor: AppColors.kPrimaryColor,
      ).copyWith(splashFactory: NoSplash.splashFactory),
    ),
    scaffoldBackgroundColor: AppColors.kBackgroundColor,
  );
}
