import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_text_form_field.dart';
import 'package:heal_link/core/widgets/custom_title_of_text_field_section.dart';

class TextFieldPart extends StatelessWidget {
  const TextFieldPart({
    super.key,
    required this.titleText,
    required this.hintText,
    required this.iconPath,
    this.isPassword = false,
    this.isSignIn = false,
    this.onForgetPass , 
    this.customValidator,
     this.controller,
  });
  final String titleText;
  final String hintText;
  final String iconPath;
  final bool isPassword;
  final bool isSignIn;
  final void Function()? onForgetPass ; 
  final String? Function(String?)? customValidator ; 
  final TextEditingController? controller; 
  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        CustomTitleOfTextFieldSection(titleText: titleText),
        SizedBox(height: 8),
        CustomTextFormField(
          controller: controller,
          hintText: hintText,
          icon: iconPath,
          isPassword: isPassword,
          validator: customValidator,
          
        ),
        SizedBox(height: 4),
        Row(
          mainAxisAlignment: MainAxisAlignment.end,
          children: [
            // CustomBottomHintOfTextFeildPart(
            //   text: "This is a hint text to help user.",
            // ),
            // Spacer(),
            isSignIn
                ? GestureDetector(
                  onTap: onForgetPass,
                  child: Text(
                    "Forget Password?",
                    style: AppTextStyles.popins400style12LightBlackColor.copyWith(
                      color: AppColors.kErrorColor,
                    ),
                  ),
                )
                : SizedBox(),
          ],
        ),
      ],
    );
  }
}
