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
    this.validator,
    this.onSaved,
    this.controller,
    this.focusNode,
    this.onTap,
    this.onChanged,
   
  });
  final String hintText;
  final String icon;
  final bool isPassword;
  final String? Function(String?)? validator;
  final TextEditingController? controller;
  final FocusNode? focusNode;
  final VoidCallback? onTap;
  final ValueChanged? onChanged;
 final void Function(String?)? onSaved;

  @override
  State<CustomTextFormField> createState() => _CustomTextFormFieldState();
}

class _CustomTextFormFieldState extends State<CustomTextFormField> {
  late bool _obscureText;

  @override
  void initState() {
    super.initState();
    _obscureText = widget.isPassword;
  }

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      controller: widget.controller,
      obscureText: widget.isPassword ? _obscureText : false,
      cursorColor: AppColors.kPrimaryColor,
      decoration: InputDecoration(
        border: formFeildBorderFunction(error: false),
        enabledBorder: formFeildBorderFunction(error: false),
        focusedBorder: formFeildBorderFunction(error: false, onFocus: true),
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
                      _obscureText ?   AppImages.eyeIcon  :AppImages.eyeSlashIcon,
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
      focusNode: widget.focusNode,
      onSaved: (String? value) {},
      onChanged: widget.onChanged,
      onTap: widget.onTap,
      validator: widget.validator,
    );
  }

  OutlineInputBorder formFeildBorderFunction({
    required bool error,
    bool onFocus = false,
  }) {
    return OutlineInputBorder(
      borderSide: BorderSide(
        color:
            error
                ? AppColors.kErrorColor
                : onFocus
                ? AppColors.kPrimaryColor
                : AppColors.kLightGreyColor,
      ),
      borderRadius: BorderRadius.circular(8),
    );
  }
}
