
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_loading_image_section.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignUpSecondForm extends StatelessWidget {
  const DoctorSignUpSecondForm({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          spacing: 16,
          children: [
            //* National ID TextField
            TextFieldPart(
              titleText: S.of(context).national_id,
              hintText: S.of(context).enter_name,
              iconPath: AppImages.userIcon,
            ),
            //* Practice Number License TextField
            TextFieldPart(
              titleText: S.of(context).practice_license_number,
              hintText: S.of(context).practice_license_number,
              iconPath: AppImages.userIcon,
            ),
            //* Upload Image Section
            DoctorSignUpUploadImageSection(),
            SizedBox(height: 16),
            //* Elevated Button
            CustomElevatedButton(
              onPressed: () {
                context.push(AppRouter.doctorSignUpVerifyEmailView);
              },
              text: S.of(context).next,
            ),
          ],
        ),
      ),
    );
  }
}
