
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorResetPasswordForm extends StatelessWidget {
  const DoctorResetPasswordForm({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        child: Column(
          children: [
            //* Email Text Field
            TextFieldPart(
              titleText: S.of(context).password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
            ),
            SizedBox(height: 16),
            //* Confirm Password TextFeild
            TextFieldPart(
              titleText: S.of(context).confirm_password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
            ),
            SizedBox(height: 32),
    
            CustomElevatedButton(
              text: S.of(context).send,
              onPressed: () {},
            ),
          ],
        ),
      ),
    );
  }
}
