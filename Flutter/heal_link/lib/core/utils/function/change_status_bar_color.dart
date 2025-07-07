import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'app_colors.dart';

void changeStatusBarColor() {
  SystemChrome.setSystemUIOverlayStyle(
     const SystemUiOverlayStyle(
      statusBarColor: Colors.transparent,
      statusBarIconBrightness: Brightness.dark,
      systemNavigationBarColor: AppColors.kWhiteColor,
      systemNavigationBarIconBrightness: Brightness.dark,
    ),
  );
}