import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

import '../utils/constant.dart';

class CustomEmptyButton extends StatelessWidget {
  final String text;
  final double height;
  final VoidCallback response;
  final TextStyle? textStyle;
  final double? borderSide;

  const CustomEmptyButton({
    super.key,
    required this.text,
    required this.response,
    this.height = 57,
    this.textStyle,
    this.borderSide,
  });

  @override
  Widget build(BuildContext context) {
    return MaterialButton(
      height: height,
      shape: RoundedRectangleBorder(
        side: BorderSide(
          color: AppColors.kPrimaryColor,
          width: borderSide ?? 1,
        ),
        borderRadius: BorderRadius.circular(30),
      ),
      minWidth: AppConstant.width,
      onPressed: response,
      child: Text(text),
    );
  }
}
