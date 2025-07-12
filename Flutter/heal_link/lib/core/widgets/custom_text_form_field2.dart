import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:heal_link/core/utils/app_styles.dart';

import '../utils/function/app_colors.dart';

class CustomTextFormField2 extends StatelessWidget {
  final String hintText;
  final TextInputType keyboardType;
  final TextEditingController controller;
  final FormFieldValidator validator;
  final String? prefixIcon;
  final String? suffixIcon;
  final double borderRadiusSize;
  final VoidCallback? suffixIconResponse;
  final VoidCallback? onTap;
  final ValueChanged? onChanged;
  final ValueChanged? onSaved;

  const CustomTextFormField2({
    super.key,
    required this.hintText,
    required this.keyboardType,
    required this.controller,
    required this.validator,
    this.prefixIcon,
    this.suffixIcon,
    this.borderRadiusSize = 12,
    this.suffixIconResponse,
    this.onChanged,
    this.onTap,
    this.onSaved,
  });

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 40,
      child: TextFormField(
        onTap: onTap,
        keyboardType: keyboardType,
        controller: controller,
        validator: validator,
        onChanged: onChanged,
        onSaved: onSaved,
        decoration: InputDecoration(
          filled: true,
          fillColor: AppColors.kWhiteColor,
          prefixIconColor: AppColors.kTextFieldIconColor,
          suffixIconColor: AppColors.kTextFieldIconColor,
          prefixIcon:
              prefixIcon != null
                  ? SvgPicture.asset(prefixIcon!, fit: BoxFit.none)
                  : null,
          suffixIcon:
              suffixIcon != null
                  ? IconButton(
                    onPressed: suffixIconResponse,
                    icon: SvgPicture.asset(suffixIcon!),
                  )
                  : null,
          focusedBorder: OutlineInputBorder(
            borderSide: BorderSide(color: AppColors.kWhiteColor),
            borderRadius: BorderRadius.circular(borderRadiusSize),
          ),
          enabledBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(borderRadiusSize),
            borderSide: BorderSide(color: AppColors.kWhiteColor),
          ),
          hintText: hintText,
          hintStyle: AppTextStyles.popins400style12kPrimaryColor.copyWith(
            color: AppColors.kTextFieldIconColor,
          ),
          border: OutlineInputBorder(borderRadius: BorderRadius.circular(10)),
        ),
      ),
    );
  }
}
