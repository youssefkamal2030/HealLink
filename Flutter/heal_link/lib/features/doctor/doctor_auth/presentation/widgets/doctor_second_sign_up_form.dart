
// import 'package:flutter/material.dart';
// import 'package:go_router/go_router.dart';
// import 'package:heal_link/core/utils/app_images.dart';
// import 'package:heal_link/core/utils/app_router.dart';
// import 'package:heal_link/core/widgets/custom_elevated_button.dart';
// import 'package:heal_link/core/widgets/text_field_part.dart';
// import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_loading_image_section.dart';
// import 'package:heal_link/generated/l10n.dart';

// class DoctorSignUpSecondForm extends StatelessWidget {
//   const DoctorSignUpSecondForm({super.key});

//   @override
//   Widget build(BuildContext context) {
//     return SliverToBoxAdapter(
//       child: Form(
//         child: Column(
//           crossAxisAlignment: CrossAxisAlignment.start,
//           spacing: 16,
//           children: [
//             //* National ID TextField
//             TextFieldPart(
//               titleText: S.of(context).national_id,
//               hintText: S.of(context).enter_national_id,
//               iconPath: AppImages.userIcon,
//               isPassword: false,
//             ),
//             //* Practice Number License TextField
//             TextFieldPart(
//               titleText: S.of(context).practice_license_number,
//               hintText: S.of(context).practice_license_number,
//               iconPath: AppImages.userIcon,
//             ),
//             //* Upload Image Section
//             DoctorSignUpUploadImageSection(),
//             SizedBox(height: 16),
//             //* Elevated Button
//             CustomElevatedButton(
//               onPressed: () {
//                 context.push(AppRouter.doctorSignUpVerifyEmailView);
//               },
//               text: S.of(context).next,
//             ),
//           ],
//         ),
//       ),
//     );
//   }
// }




import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/core/widgets/text_field_part.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_loading_image_section.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignUpSecondForm extends StatefulWidget {
  const DoctorSignUpSecondForm({super.key});

  @override
  State<DoctorSignUpSecondForm> createState() => _DoctorSignUpSecondFormState();
}

class _DoctorSignUpSecondFormState extends State<DoctorSignUpSecondForm> {
  final _formKey = GlobalKey<FormState>();

  final TextEditingController _nationalIdController = TextEditingController();
  final TextEditingController _practiceNumberController = TextEditingController();

  @override
  void dispose() {
    _nationalIdController.dispose();
    _practiceNumberController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        key: _formKey,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            //* National ID TextField
            TextFieldPart(
              titleText: S.of(context).national_id,
              hintText: S.of(context).enter_national_id,
              iconPath: AppImages.userIcon,
              isPassword: false,
              controller: _nationalIdController,
              customValidator: (value) {
                if (value == null || value.trim().isEmpty) {
                  // return S.of(context).please_enter_national_id;
                  return "Please enter national ID";
                }
                if (value.length != 14) {
                  //return S.of(context).national_id_must_be_14_digits;
                  return "National ID must be 14 digits";
                }
                return null;
              },
            ),
            const SizedBox(height: 16),
            //* Practice License Number TextField
            TextFieldPart(
              titleText: S.of(context).practice_license_number,
              hintText: S.of(context).practice_license_number,
              iconPath: AppImages.userIcon,
              controller: _practiceNumberController,
              customValidator: (value) {
                if (value == null || value.trim().isEmpty) {
                  // return S.of(context).please_enter_practice_license_number;
                  return "Please enter practice license number";
                }
                if (value.length < 4) {
                  // return S.of(context).license_number_too_short;
                  return "License number too short";
                }
                return null;
              },
            ),
            const SizedBox(height: 16),
            //* Upload Image Section
            const DoctorSignUpUploadImageSection(),
            const SizedBox(height: 24),
            //* Elevated Button
            CustomElevatedButton(
              onPressed: () {
                if (_formKey.currentState!.validate()) {
                  // Navigate if valid
                  context.push(AppRouter.doctorSignUpVerifyEmailView);
                }
              },
              text: S.of(context).next,
            ),
          ],
        ),
      ),
    );
  }
}
