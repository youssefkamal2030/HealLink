
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_forget_pass_try_anther_way_button.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorForgetPasswordForm extends StatelessWidget {
  const DoctorForgetPasswordForm({
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
              titleText: S.of(context).email,
              hintText: S.of(context).enter_email,
              iconPath: AppImages.smsIcon,
            ),
            SizedBox(height: 16),
            DoctorForgetPassTryAntherWayButton(),
            SizedBox(height: 32),
            CustomElevatedButton(text: S.of(context).send , 
            onPressed: () {
                     context.push(AppRouter.doctorResetPasswordView);
            },
            ),
          ],
        ),
      ),
    );
  }
}
