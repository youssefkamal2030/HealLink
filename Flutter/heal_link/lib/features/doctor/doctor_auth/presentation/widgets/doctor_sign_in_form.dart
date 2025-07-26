
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignInForm extends StatelessWidget {
  const DoctorSignInForm({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        child: Column(
          children: [
            //* Email TextField
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
              isSignIn: true,
              onForgetPass: (){
                   context.push(AppRouter.doctorForgetPasswordView);
              },
            ),
            SizedBox(height: 32),
            CustomElevatedButton(
              text: S.of(context).sign_in,
              onPressed: () {
                    context.push(AppRouter.doctorHomeView,
                    
                    );
              },
            ),
          ],
        ),
      ),
    );
  }
}
