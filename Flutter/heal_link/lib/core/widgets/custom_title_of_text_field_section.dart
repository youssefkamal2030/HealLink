import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class CustomTitleOfTextFieldSection extends StatelessWidget {
  const CustomTitleOfTextFieldSection({
    super.key,
    required this.titleText,
  });

  final String titleText;

  @override
  Widget build(BuildContext context) {
    return Text(titleText, style: AppTextStyles.roboto500style14BlackColor);
  }
}
