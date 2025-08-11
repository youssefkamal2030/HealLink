
// import 'package:flutter/material.dart';
// import 'package:go_router/go_router.dart';
// import 'package:heal_link/core/utils/app_images.dart';
// import 'package:heal_link/core/utils/app_router.dart';
// import 'package:heal_link/core/widgets/custom_elevated_button.dart';
// import 'package:heal_link/core/widgets/text_field_part.dart';
// import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_forget_pass_try_anther_way_button.dart';
// import 'package:heal_link/generated/l10n.dart';

// class DoctorForgetPasswordForm extends StatelessWidget {
//   const DoctorForgetPasswordForm({
//     super.key,
//   });

//   @override
//   Widget build(BuildContext context) {
//     return SliverToBoxAdapter(
//       child: Form(
//         child: Column(
//           children: [
//             //* Email Text Field
//             TextFieldPart(
//               titleText: S.of(context).email,
//               hintText: S.of(context).enter_email,
//               iconPath: AppImages.smsIcon,
//             ),
//             SizedBox(height: 16),
//             DoctorForgetPassTryAntherWayButton(),
//             SizedBox(height: 32),
//             CustomElevatedButton(text: S.of(context).send , 
//             onPressed: () {
//                      context.push(AppRouter.doctorResetPasswordView);
//             },
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
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_forget_pass_try_anther_way_button.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorForgetPasswordForm extends StatefulWidget {
  const DoctorForgetPasswordForm({super.key});

  @override
  State<DoctorForgetPasswordForm> createState() => _DoctorForgetPasswordFormState();
}

class _DoctorForgetPasswordFormState extends State<DoctorForgetPasswordForm> {
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();

  @override
  void dispose() {
    _emailController.dispose();
    super.dispose();
  }

  void _submit() {
    if (_formKey.currentState!.validate()) {
      context.push(AppRouter.doctorResetPasswordView);
    }
  }

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Form(
        key: _formKey,
        child: Column(
          children: [
            //* Email Text Field
            TextFieldPart(
              controller: _emailController,
              titleText: S.of(context).email,
              hintText: S.of(context).enter_email,
              iconPath: AppImages.smsIcon,
              customValidator: (value) {
                if (value == null || value.isEmpty) {
                  // return S.of(context).please_enter_email;
           return "Please enter email";
                }
                return null;
              },
            ),

            const SizedBox(height: 16),
            const DoctorForgetPassTryAntherWayButton(),
            const SizedBox(height: 32),

            CustomElevatedButton(
              text: S.of(context).send,
              onPressed: _submit,
            ),
          ],
        ),
      ),
    );
  }
}
