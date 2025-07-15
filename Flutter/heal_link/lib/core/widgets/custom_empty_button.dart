import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

import '../utils/constant.dart';

class CustomEmptyButton extends StatelessWidget {
  final String text;
  final double height;
  final VoidCallback response;
  final TextStyle? textStyle;
  final double? borderSide;
  final double? borderRadius;
  final double? iconSize;
  final Color? borderColor;

  const CustomEmptyButton({
    super.key,
    required this.text,
    required this.response,
    this.height = 44,
    this.textStyle,
    this.borderSide,
    this.borderRadius,
    this.iconSize,
    this.borderColor = AppColors.kPrimaryColor,
  });

  @override
  Widget build(BuildContext context) {
    return MaterialButton(
      elevation: 0,
      height: height,
      color: Colors.white,
      shape: RoundedRectangleBorder(
        side: BorderSide(color: borderColor!, width: borderSide ?? 1),
        borderRadius: BorderRadius.circular(borderRadius ?? 16),
      ),
      minWidth: AppConstant.width,
      onPressed: response,
      padding: EdgeInsets.all(8),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Text(
            text,
            style:
                textStyle ??
                AppTextStyles.popins500style18LightBlackColor.copyWith(
                  fontSize: 16,
                  color: borderColor,
                ),
          ),
          if (iconSize != null) SizedBox(width: 4),
          if (iconSize != null)
            Icon(
              Icons.arrow_forward_ios_rounded,
              color: borderColor,
              size: iconSize,
            ),
        ],
      ),
    );
  }
}
