import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class CustomTextFormField extends StatefulWidget {
   const CustomTextFormField({
    super.key,
    required this.hintText,
    required this.icon,
    this.isPassword = false,
    this.validator , 
    this.onSaved, 
  });
  final String hintText;
  final String icon;
  final bool isPassword;
  final String? Function(String?)? validator ; 
  final void Function(String?)? onSaved ; 

  @override
  State<CustomTextFormField> createState() => _CustomTextFormFieldState();
}

class _CustomTextFormFieldState extends State<CustomTextFormField> {
  bool _obscureText = true;
  @override
  Widget build(BuildContext context) {
    return TextFormField(
      obscureText: _obscureText,
      cursorColor: AppColors.kDarkGreyColor,
      decoration: InputDecoration(
        border: formFeildBorderFunction(error: false),
        enabledBorder: formFeildBorderFunction(error: false),
        focusedBorder: formFeildBorderFunction(error: false),
        errorBorder: formFeildBorderFunction(error: true),
        prefixIcon: Padding(
          padding: const EdgeInsets.all(8.0),
          child: SvgPicture.asset(widget.icon, height: 24, width: 24),
        ),
        suffixIcon:
            widget.isPassword
                ? GestureDetector(
                  onTap: () {
                    setState(() {
                      _obscureText = !_obscureText;
                    });
                  },
                  child: Padding(
                    padding: EdgeInsets.all(8.0),
                    child: SvgPicture.asset(
                      _obscureText ? AppImages.eyeSlashIcon : AppImages.eyeIcon,
                      height: 24,
                      width: 24,
                    ),
                  ),
                )
                : null,

        hintText: widget.hintText,
        hintStyle: AppTextStyles.popins400style14LightBlackColor.copyWith(
          color: AppColors.kDarkGreyColor,
        ),
      ),

      onSaved: (String? value) {},
      validator: (String? value) {
        return null;
      },
    );
  }

  OutlineInputBorder formFeildBorderFunction({required bool error}) {
    return OutlineInputBorder(
      borderSide: BorderSide(
        color: error ? AppColors.kErrorColor : AppColors.kLightGreyColor,
      ),
      borderRadius: BorderRadius.circular(8),
    );
  }
}
