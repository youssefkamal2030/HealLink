import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorFirstSignupForm extends StatefulWidget {
  const DoctorFirstSignupForm({super.key});

  @override
  State<DoctorFirstSignupForm> createState() => _DoctorFirstSignupFormState();
}

class _DoctorFirstSignupFormState extends State<DoctorFirstSignupForm> {
  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          spacing: 16,
          children: [
            //* Name TextField
            TextFieldPart(
              titleText: S.of(context).name,
              hintText: S.of(context).enter_name,
              iconPath: AppImages.personIcon,
            ),
            //* Email TextFeild
            TextFieldPart(
              titleText: S.of(context).email,
              hintText: S.of(context).enter_email,
              iconPath: AppImages.smsIcon,
            ),
            //* Password TextField
            TextFieldPart(
              titleText: S.of(context).password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
            ),
      
            //* Confirm Password
            TextFieldPart(
              titleText: S.of(context).confirm_password,
              hintText: S.of(context).enter_password,
              iconPath: AppImages.keyIcon,
              isPassword: true,
            ),
            SizedBox(height: 16),
      
            //* Elevated Button
            CustomElevatedButton(
              onPressed: () {
                context.push(AppRouter.doctorSignUpSecondView);
              },
              text: S.of(context).next,
            ),
          ],
        ),
      ),
    );
  }
}
